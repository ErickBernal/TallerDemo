<template>
  <q-card>
    <q-card-section class="bg-teal text-white">
      <div class="text-h5 q-mt-sm q-mb-xs">Modificar Vehiculos</div>
    </q-card-section>

    <q-card class="q-mb-md" no-border>
      <q-card-section>
        <div class="q-gutter-md q-mb-md">
          <q-input v-model="filterText" placeholder="Buscar vehiculo..." />
        </div>
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

          <!--  -->
        </q-table>
      </q-card-section>
    </q-card>

    <!--  -->
    <q-dialog v-model="editDialogVisible">
      <q-card>
        <q-card-section>
          <q-input
            v-model="editedRow.placa"
            label="Nombre"
            filled
            type="string"
            mask="string"
            lazy-rules
            :rules="[(val) => (val && val.length > 0) || 'Ingrese un Nombre']"
          />

          <q-select
            rounded
            outlined
            v-model="editedRow.linea"
            :options="vehicleLineas"
            label="Linea de vehiculo"
          />

          <!--  -->
        </q-card-section>
        <q-card-actions align="right">
          <q-btn label="Cancelar" color="negative" @click="closeEditDialog" />
          <q-btn label="Guardar" color="primary" @click="saveChanges" />
        </q-card-actions>
      </q-card>
    </q-dialog>
    <!--  -->

    <q-dialog v-model="deleteDialogVisible">
      <q-card>
        <q-card-section>
          <q-card-section>
            <p><strong>Placa:</strong> {{ editedRow.placa }}</p>
          </q-card-section>
          <q-card-section>
            <p><strong>Color:</strong> {{ editedRow.color }}</p>
          </q-card-section>
          <q-card-section>
            <p><strong>Tipo:</strong> {{ editedRow.type }}</p>
          </q-card-section>
        </q-card-section>
        <q-card-actions align="right">
          <q-btn label="Cancelar" color="negative" @click="closeEditDialog" />
          <q-btn label="Eliminar" color="primary" @click="deleteVehicle" />
        </q-card-actions>
      </q-card>
    </q-dialog>
    <!--  -->
  </q-card>
</template>

<script setup>
import { ref, onMounted, computed } from "vue";
import axios from "axios";
import { useQuasar } from "quasar";

const $q = useQuasar();
const loading = ref(false);
const rows = ref([]);
const vehicleLineas = ref([]);
const onlyOneGet = ref(true);
const columns = [
  {
    name: "placa",
    align: "left",
    label: "Placa",
    field: "placa",
    sortable: true,
  },
  {
    name: "color",
    align: "left",
    label: "Color",
    field: "color",
    sortable: true,
  },
  {
    name: "type",
    align: "center",
    label: "Tipo",
    field: "type",
    sortable: false,
  },
  {
    name: "linea",
    align: "center",
    label: "Linea",
    field: "Linea",
    sortable: false,
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
      row.placa.toLowerCase().includes(filterText.value.toLowerCase()) ||
      row.name
        .toString()
        .toLowerCase()
        .includes(filterText.value.toLowerCase()) ||
      row.lastName
        .toString()
        .toLowerCase()
        .includes(filterText.value.toLowerCase())
  );
});

let selectedRow = null;
const editDialogVisible = ref(false);
const deleteDialogVisible = ref(false);
const editedRow = ref({
  name: "",
  lastName: "",
  placa: "",
  linea: "",
  lineaId: 0,
});

async function getVehicles() {
  loading.value = true;
  try {
    const url = process.env.API_BASE_URL + "/taller/Vehicle";
    const response = await axios.get(url);
    // console.log("//******>" + JSON.stringify(response.data));
    rows.value = response.data.map((res) => ({
      id: res.id,
      placa: res.placa,
      vehicleLineaId: res.vehicleLineaId,
      color: res.vehicleLinea.color,
      type: res.vehicleLinea.type,
      Linea: res.vehicleLinea.line,
      actions: null,
    }));
    // console.log("//---->" + JSON.stringify(rows));
  } catch (error) {
    console.error("Error al obtener datos:", error);
  } finally {
    loading.value = false;
  }
}

async function getVehicleLinea() {
  if (onlyOneGet.value) {
    loading.value = true;
    try {
      const url = process.env.API_BASE_URL + "/taller/VehicleLine";
      const response = await axios.get(url);
      vehicleLineas.value = response.data.map((res) => ({
        id: res.id,
        color: res.color,
        type: res.type,
        line: res.line,
        label: res.type + " " + res.line + " " + res.color,
      }));
      //   console.log("//-->::::" + JSON.stringify(vehicleLineas.value));
    } catch (error) {
      console.error("Error al obtener datos (vehicleLinea):", error);
    } finally {
      loading.value = false;
    }
    onlyOneGet.value = false;
  }
}

function start() {
  getVehicles();
}

const showTable = ref(false);

function toggleTable() {
  showTable.value = !showTable.value;
}

function openEditDialog(row) {
  selectedRow = row;
  editedRow.value = { ...row };

  editDialogVisible.value = true;
  getVehicleLinea();
}

function closeEditDialog() {
  selectedRow = null;
  editDialogVisible.value = false;
}

function openDeleteDialog(row) {
  selectedRow = row;
  editedRow.value = { ...row };
  deleteDialogVisible.value = true;
}

function closeDeleteDialog() {
  selectedRow = null;
  deleteDialogVisible.value = false;
}

async function deleteVehicle() {
  try {
    const url =
      process.env.API_BASE_URL + "/taller/Vehicle?id=" + selectedRow.id;
    console.log("---->..." + url);
    await axios.delete(url);
    getVehicles();
    closeDeleteDialog();
  } catch (error) {
    console.error("Error al eliminar vehiculo:", error);
    alert("Error al eliminar vehiculo:");
  }
}

async function saveChanges() {
  if (editedRow.value.linea == null) {
    editedRow.value.linea = {
      id: editedRow.value.vehicleLineaId,
    };
  }
  try {
    const url = process.env.API_BASE_URL + "/taller/Vehicle";
    await axios.put(url, {
      id: editedRow.value.id,
      placa: editedRow.value.placa,
      vehicleLineaId: editedRow.value.linea.id,
    });
    getVehicles();
    closeEditDialog();
  } catch (error) {
    console.error("Error al guardar los cambios:", error);
  }
}

function selectedStateLabel() {
  const selectedOption = stateOptions.find(
    (option) => option.value === editedRow.value.state
  );
  return selectedOption ? selectedOption.label : "";
}

onMounted(start);
</script>
