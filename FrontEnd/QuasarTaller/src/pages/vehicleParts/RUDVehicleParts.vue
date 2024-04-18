<template>
  <!--  -->
  <!--  -->
  <div class="row q-col-gutter-sm q-ma-xs">
    <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
      <!-- <q-dialog v-model="isActiveFindVehicle"> -->

      <q-card class="my-card">
        <!--  -->
        <q-card-section class="bg-grey-8 text-white">
          <div class="text-h6">Buscar repuesto</div>
        </q-card-section>
        <!--  -->
        <q-card-section>
          <div class="q-gutter-md">
            <div class="q-gutter-md q-mb-md">
              <q-input v-model="filterText" placeholder="Buscar..." />
              <!-- <q-btn @click="filterData" label="Filtrar" color="primary" /> -->
            </div>
            <!--  -->
            <q-table
              :rows="filteredRows"
              :columns="columns"
              row-key="id"
              :loading="loading"
              no-data-label="No hay datos disponibles"
            >
              <!-- Columna para botones CRUD -->
              <template #body-cell-update="props">
                <q-td :props="props">
                  <q-btn icon="edit" @click="loadEditComponent(props.row)" />
                </q-td>
              </template>
              <!-- Columna para botones CRUD -->
              <template #body-cell-delete="props">
                <q-td :props="props">
                  <q-btn icon="delete" @click="openDeleteDialog(props.row)" />
                </q-td>
              </template>

              <template #body-cell-compatible="props">
                <q-td :props="props">
                  <!-- <q-btn icon="delete" @click="openDeleteDialog(props.row)" /> -->
                  <q-select
                    filled
                    v-model="model"
                    label="Compatible"
                    :options="VPCompatibles"
                    :option-value="'id'"
                    :option-label="optionLabelTemplate"
                    behavior="menu"
                  ></q-select>
                </q-td>
              </template>
            </q-table>
            <!--  -->
          </div>
        </q-card-section>
        <!--  -->
      </q-card>
      <br />
    </div>

    <!--  -->
    <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
      <NewVehiclePart
        v-model:editedRow="editedRow"
        v-model:editMode="editMode"
      />
    </div>
  </div>

  <!-- pop ups--------------------------------------------------------------------------------- -->
  <q-dialog v-model="deleteDialogVisible">
    <q-card>
      <q-card-section class="bg-grey-8 text-white">
        <div class="text-h5 q-mt-sm q-mb-xs">Eliminar repuesto</div>
      </q-card-section>
      <q-card-section>
        <q-card-section>
          <p><strong>Nombre:</strong> {{ editedRow.name }}</p>
        </q-card-section>
        <q-card-section>
          <p><strong>Stock:</strong> {{ editedRow.stock }}</p>
        </q-card-section>
        <q-card-section>
          <p><strong>Precio unitario:</strong> {{ editedRow.unitPrice }}</p>
        </q-card-section>
      </q-card-section>
      <q-card-actions align="right">
        <q-btn label="Cancelar" color="negative" @click="closeEditDialog" />
        <q-btn label="Eliminar" color="primary" @click="deleteVehicleParts" />
      </q-card-actions>
    </q-card>
  </q-dialog>
</template>

<script setup>
import { ref, onMounted, computed } from "vue";
import axios from "axios";
import { useQuasar } from "quasar";
import NewVehiclePart from "./NewVehiclePart.vue";

//props using v-model
const editMode = ref(false);
const editedRow = ref({ id: 0, name: "", stock: 0, unitPrice: 0 });

//
let selectedRow = null;
const editDialogVisible = ref(false);
const deleteDialogVisible = ref(false);
const makeChanges = ref(false);
function closeEditDialog() {
  selectedRow = null;
  editDialogVisible.value = false;
  editedRow.value.name = "";
  editedRow.value.stock = 0;
  editedRow.value.unitPrice = 0;
  editMode.value = false;
}
//
const { localStorage, notify } = useQuasar();
const loading = ref(false);
const rows = ref([]);
const VPCompatibles = ref([]);
const model = ref();

const isActiveFindVehicle = ref(true);

const columns = [
  {
    name: "Name",
    align: "center",
    label: "Nombre",
    field: "name",
    sortable: true,
  },
  {
    name: "Stock",
    align: "center",
    label: "Stock",
    field: "stock",
    sortable: true,
  },
  {
    name: "precio",
    align: "center",
    label: "Precio Unitario",
    field: "unitPrice",
    sortable: true,
  },
  {
    name: "compatible",
    align: "center",
    label: "Compatible",
    field: "compatible",
    sortable: false,
  },

  {
    name: "update",
    align: "center",
    label: "Modificar",
    field: "update",
    sortable: false,
  },
  {
    name: "delete",
    align: "center",
    label: "Eliminar",
    field: "delete",
    sortable: false,
  },
];
const filterText = ref("");
const filteredRows = computed(() => {
  return rows.value.filter(
    (row) =>
      row.name.toLowerCase().includes(filterText.value.toLowerCase()) ||
      row.stock
        .toString()
        .toLowerCase()
        .includes(filterText.value.toLowerCase()) ||
      row.unitPrice
        .toString()
        .toLowerCase()
        .includes(filterText.value.toLowerCase())
  );
});

async function getVehicleParts() {
  loading.value = true;
  try {
    const url = process.env.API_BASE_URL + "/taller/VehicleParts";
    const response = await axios.get(url);
    rows.value = response.data.map((res) => ({
      id: res.id,
      name: res.name,
      stock: res.stock,
      unitPrice: res.unitPrice,
    }));
  } catch (error) {
    console.error("Error al obtener datos:", error);
  } finally {
    loading.value = false;
  }
}

async function getVehicleCompatibleParts() {
  loading.value = true;
  try {
    const url = process.env.API_BASE_URL + "/taller/VPCompatible";
    const response = await axios.get(url);
    VPCompatibles.value = response.data;
  } catch (error) {
    console.error("Error al obtener datos:", error);
  } finally {
    loading.value = false;
  }
}

async function deleteVehicleParts() {
  try {
    const url =
      process.env.API_BASE_URL + "/taller/VehicleParts?id=" + selectedRow.id;
    await axios.delete(url);
    closeDeleteDialog();
    getVehicleParts();
  } catch (error) {
    console.error("Error al eliminar el trabajo:", error);
  }
  editedRow.value.name = "";
  editedRow.value.stock = 0;
  editedRow.value.unitPrice = 0;
  editMode.value = false;
}

function loadEditComponent(row) {
  selectedRow = row;
  editedRow.value = { ...row };
  editMode.value = true;
}

function openDeleteDialog(row) {
  selectedRow = row;
  editedRow.value = { ...row };
  deleteDialogVisible.value = true;
}

function closeDeleteDialog() {
  selectedRow = null;
  deleteDialogVisible.value = false;
  editedRow.value.name = "";
  editedRow.value.stock = 0;
  editedRow.value.unitPrice = 0;
}

const optionLabelTemplate = (option) => {
  return `${option.vehicleLinea.color} ${option.vehicleLinea.type} ${option.vehicleLinea.line}`;
};

function start() {
  getVehicleParts();
  getVehicleCompatibleParts();
  localStorage.set("owner", false);
}

onMounted(start);
</script>
