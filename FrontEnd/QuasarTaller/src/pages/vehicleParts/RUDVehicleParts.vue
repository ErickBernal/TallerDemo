<template>
  <!--  -->
  <!--  -->
  <!-- <q-dialog v-model="isActiveFindVehicle"> -->
  <q-card class="my-card">
    <!--  -->
    <q-card-section class="bg-grey-8 text-white">
      <div class="text-h6">Buscar repuesto</div>
    </q-card-section>
    <!--  -->
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
          <template #body-cell-CRUD="props">
            <q-td :props="props">
              <q-btn icon="edit" @click="openEditDialog(props.row)" />
              <q-btn icon="delete" @click="openDeleteDialog(props.row)" />
            </q-td>
          </template>
        </q-table>
        <!--  -->
      </div>
    </q-card-section>
    <!--  -->
    <!--  -->
  </q-card>
  <!-- </q-dialog> -->
  <!--  -->
  <q-dialog v-model="editDialogVisible">
    <q-card>
      <q-card-section class="bg-grey-8 text-white">
        <div class="text-h5 q-mt-sm q-mb-xs">Editar repuesto</div>
      </q-card-section>
      <q-card-section>
        <q-form @submit="submitForm" class="q-gutter-md">
          <q-input
            v-model="editedRow.name"
            label="Nombre"
            filled
            type="string"
            mask="string"
            lazy-rules
            :rules="[(val) => (val && val.length > 0) || 'Ingrese un Nombre']"
          />
          <q-input
            v-model="editedRow.stock"
            label="Cantidad"
            filled
            type="string"
            mask="string"
            lazy-rules
            :rules="[
              (val) => (val && val.length > 0) || 'Ingrese una cantidad',
            ]"
          />
          <q-input
            v-model="editedRow.unitPrice"
            label="Precio Unitario"
            filled
            type="string"
            mask="string"
            lazy-rules
            :rules="[
              (val) => (val && val.length > 0) || 'Ingrese precio unitario',
            ]"
          />
          <q-card-actions align="right">
            <q-btn label="Cancelar" color="negative" @click="closeEditDialog" />
            <q-btn label="Guardar" color="primary" @click="saveChanges" />
          </q-card-actions>
        </q-form>
      </q-card-section>
    </q-card>
  </q-dialog>
  <!--  -->
  <!--  -->

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
  <!--  -->
  <!--  -->
</template>

<script setup>
import { ref, onMounted, computed } from "vue";
import axios from "axios";
import { useQuasar } from "quasar";

let selectedRow = null;
const editDialogVisible = ref(false);
const deleteDialogVisible = ref(false);
const editedRow = ref({ name: "", stock: 0, unitPrice: 0 });
function closeEditDialog() {
  selectedRow = null;
  editDialogVisible.value = false;
}
//
const { localStorage, notify } = useQuasar();
const loading = ref(false);
const rows = ref([]);
const isActiveFindVehicle = ref(true);

function openEditDialog(row) {
  selectedRow = row;
  editedRow.value = { ...row };
  editDialogVisible.value = true;
}
const newVehiclePat = ref({
  name: "",
  stock: "",
  unitPrice: "",
});

function openDeleteDialog(row) {
  selectedRow = row;
  editedRow.value = { ...row };
  deleteDialogVisible.value = true;
}

const columns = [
  {
    name: "Name",
    align: "left",
    label: "Nombre",
    field: "name",
    sortable: true,
  },
  {
    name: "Stock",
    align: "left",
    label: "Stock",
    field: "stock",
    sortable: true,
  },
  {
    name: "precio",
    align: "left",
    label: "Precio Unitario",
    field: "unitPrice",
    sortable: true,
  },
  {
    name: "CRUD",
    align: "center",
    label: "Editar",
    field: "CRUD",
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

async function saveChanges() {
  try {
    const url = process.env.API_BASE_URL + "/taller/VehicleParts";
    await axios.put(url, {
      id: editedRow.value.id,
      name: editedRow.value.name,
      stock: editedRow.value.stock,
      unitPrice: editedRow.value.unitPrice,
    });
    closeEditDialog();
    getVehicleParts();
  } catch (error) {
    console.error("Error al guardar los cambios:", error);
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
}

function closeDeleteDialog() {
  selectedRow = null;
  deleteDialogVisible.value = false;
}

function start() {
  getVehicleParts();
  localStorage.set("owner", false);
}

onMounted(start);
</script>
