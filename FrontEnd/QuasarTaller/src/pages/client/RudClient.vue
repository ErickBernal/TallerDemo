<template>
  <q-card>
    <q-card-section
      class="bg-teal text-white"
      style="display: flex; justify-content: space-between; align-items: center"
    >
      <div class="text-h5 q-mt-sm q-mb-xs">Usuarios</div>
      <q-btn class="bg-grey-8" label="Crear usuario" @click="openCreateUser" />
    </q-card-section>

    <q-card class="q-mb-md" no-border>
      <q-card-section>
        <div class="q-gutter-md q-mb-md">
          <q-input v-model="filterText" placeholder="Buscar usuario..." />
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
    <q-dialog v-model="createDialogVisible">
      <div class="row q-col-gutter-sm q-ma-xs">
        <NewClient componentName="Create nuevo usuario" />
      </div>
    </q-dialog>
    <!--  -->
  </q-card>
</template>

<script setup>
import { ref, onMounted, computed } from "vue";
import axios from "axios";
import { useQuasar } from "quasar";
import NewClient from "./NewClient.vue";

const $q = useQuasar();
const loading = ref(false);
const rows = ref([]);
const Clients = ref([]);
const onlyOneGet = ref(true);
const columns = [
  {
    name: "name",
    align: "left",
    label: "Nombre",
    field: "name",
    sortable: true,
  },
  {
    name: "lastName",
    align: "left",
    label: "Apellido",
    field: "lastName",
    sortable: true,
  },
  {
    name: "DPI",
    align: "center",
    label: "Dpi",
    field: "dpi",
    sortable: false,
  },
  {
    name: "nit",
    align: "center",
    label: "NIT",
    field: "nit",
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
      row.name.toLowerCase().includes(filterText.value.toLowerCase()) ||
      row.lastName
        .toString()
        .toLowerCase()
        .includes(filterText.value.toLowerCase())
  );
});

let selectedRow = null;
const editDialogVisible = ref(false);
const deleteDialogVisible = ref(false);
const createDialogVisible = ref(false);

const editedRow = ref({
  name: "",
  lastName: "",
  placa: "",
  linea: "",
  lineaId: 0,
});

async function getClients() {
  if (onlyOneGet.value) {
    loading.value = true;
    try {
      const url = process.env.API_BASE_URL + "/taller/Client";
      const response = await axios.get(url);
      //   rows.value = response.data.map((res) => ({
      //     id: res.id,
      //     name: res.name,
      //     lastName: res.lastName,
      //     DPI: res.dpi,
      //     typeClient: res.typeClient.type,
      //   }));
      rows.value = response.data;
    } catch (error) {
      console.error("Error al obtener datos (vehicleLinea):", error);
    } finally {
      loading.value = false;
    }
    onlyOneGet.value = false;
  }
}

function start() {
  getClients();
}

function openCreateUser() {
  createDialogVisible.value = true;
}

function openEditDialog(row) {
  selectedRow = row;
  editedRow.value = { ...row };

  editDialogVisible.value = true;
  console.log("--21>" + JSON.stringify(editedRow.value));
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
    getClients();
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
    getClients();
    closeEditDialog();
  } catch (error) {
    console.error("Error al guardar los cambios:", error);
  }
}

onMounted(start);
</script>
