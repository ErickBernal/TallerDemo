<template>
  <div class="text-h5 q-mt-sm q-mb-xs">Modificar Servicios</div>
  <!-- <q-page class="q-pa-md"> -->
  <!-- Botón para desplegar la tabla -->

  <!-- <div v-if="showTable" class="q-gutter-md"> -->
  <div class="q-gutter-md q-mb-md">
    <q-input v-model="filterText" placeholder="Buscar..." />
  </div>
  <q-table
    :rows="filteredRows"
    :columns="columns"
    row-key="id"
    :loading="loading"
    no-data-label="No hay datos disponibles"
  >
    <!-- Columna para botones de acciones -->
    <template #body-cell-actions="props">
      <q-td :props="props">
        <q-btn icon="edit" @click="openEditDialog(props.row)" />
        <q-btn icon="delete" @click="openDeleteDialog(props.row)" />
      </q-td>
    </template>
  </q-table>
  <!-- </div> -->
  <br />

  <!-- Ventana emergente para editar -->
  <q-dialog v-model="editDialogVisible">
    <q-card>
      <q-card-section>
        <!-- Contenido de la ventana emergente de edición -->
        <q-card>
          <q-card-section>
            <q-input v-model="editedRow.work" label="Nombre del trabajo" />
            <q-input v-model="editedRow.priceWork" label="Precio" />
          </q-card-section>
          <q-card-actions align="right">
            <q-btn label="Cancelar" color="negative" @click="closeEditDialog" />
            <q-btn label="Guardar" color="primary" @click="saveChanges" />
          </q-card-actions>
        </q-card>
      </q-card-section>
    </q-card>
  </q-dialog>

  <!-- Ventana emergente para eliminar -->
  <q-dialog v-model="deleteDialogVisible">
    <q-card>
      <q-card-section>
        <!-- Contenido de la ventana emergente de eliminación -->
        <q-card>
          <q-card-section>
            ¿Estás seguro de que deseas eliminar este trabajo?
          </q-card-section>
          <q-card-actions align="right">
            <q-btn
              label="Cancelar"
              color="negative"
              @click="closeDeleteDialog"
            />
            <q-btn label="Eliminar" color="primary" @click="deleteRow" />
          </q-card-actions>
        </q-card>
      </q-card-section>
    </q-card>
  </q-dialog>
  <!-- </q-page> -->
</template>

<script setup>
import { ref, onMounted, computed } from "vue";
import axios from "axios";
import { useQuasar } from "quasar";

const $q = useQuasar();
const loading = ref(false);
const rows = ref([]);
const columns = [
  {
    name: "work",
    align: "left",
    label: "Nombre del trabajo",
    field: "work",
    sortable: true,
  },
  {
    name: "priceWork",
    align: "left",
    label: "Precio",
    field: "priceWork",
    sortable: true,
  },
  // Columna para botones de acciones
  {
    name: "actions",
    align: "center",
    label: "Acciones",
    field: "actions",
    sortable: false,
  },
];

const filterText = ref("");
const filteredRows = computed(() => {
  return rows.value.filter(
    (row) =>
      row.km.toLowerCase().includes(filterText.value.toLowerCase()) ||
      row.state
        .toString()
        .toLowerCase()
        .includes(filterText.value.toLowerCase())
  );
});

let selectedRow = null;
const editDialogVisible = ref(false);
const deleteDialogVisible = ref(false);
const editedRow = ref({ work: "", priceWork: "" });

async function fetchDataWork() {
  loading.value = true;
  try {
    const url = process.env.API_BASE_URL + "/taller/ServiceDetalle";
    const response = await axios.get(url);
    rows.value = response.data.map((res) => ({
      id: res.id,
      state: res.state,
      km: res.km,
      serviceTypeId: res.serviceTypeId,
      vehicleId: res.vehicleId,
      clientId: res.clientId,
    }));
  } catch (error) {
    console.error("Error al obtener datos:", error);
  } finally {
    loading.value = false;
  }
}

function start() {
  fetchDataWork();
  $q.localStorage.set("owner", false);
}

// Variable para controlar la visibilidad de la tabla
const showTable = ref(false);
// Función para mostrar u ocultar la tabla
function toggleTable() {
  showTable.value = !showTable.value;
}

// Método para abrir la ventana emergente de edición
function openEditDialog(row) {
  selectedRow = row;
  editedRow.value = { ...row };
  editDialogVisible.value = true;
}

// Método para cerrar la ventana emergente de edición
function closeEditDialog() {
  selectedRow = null;
  editDialogVisible.value = false;
}

// Método para abrir la ventana emergente de eliminación
function openDeleteDialog(row) {
  selectedRow = row;
  deleteDialogVisible.value = true;
}

// Método para cerrar la ventana emergente de eliminación
function closeDeleteDialog() {
  selectedRow = null;
  deleteDialogVisible.value = false;
}

// Método para eliminar la fila
async function deleteRow() {
  try {
    const url =
      process.env.API_BASE_URL + "/taller/ServiceWork?id=" + selectedRow.id;
    await axios.delete(url);
    fetchDataWork(); // Actualiza los datos después de la eliminación
    closeDeleteDialog();
  } catch (error) {
    console.error("Error al eliminar el trabajo:", error);
  }
}

async function saveChanges() {
  try {
    const url = process.env.API_BASE_URL + "/taller/ServiceWork";
    await axios.put(url, {
      id: editedRow.value.id,
      work: editedRow.value.work,
      priceWork: editedRow.value.priceWork,
      serviceListWorks: [],
      detalleWorkServices: [],
    });
    fetchDataWork();
    closeEditDialog();
  } catch (error) {
    console.error("Error al guardar los cambios:", error);
  }
}

onMounted(start);
</script>
