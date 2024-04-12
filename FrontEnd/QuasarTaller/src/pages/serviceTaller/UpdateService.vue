<template>
  <q-card>
    <q-card-section class="bg-teal text-white">
      <div class="text-h5 q-mt-sm q-mb-xs">Modificar Servicios</div>
    </q-card-section>

    <q-card class="q-mb-md" no-border>
      <q-card-section>
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
          <!-- Columna para botones CRUD -->
          <template #body-cell-CRUD="props">
            <q-td :props="props">
              <q-btn icon="edit" @click="openEditDialog(props.row)" />
              <q-btn icon="delete" @click="openDeleteDialog(props.row)" />
            </q-td>
          </template>
          <!-- columna para estados -->
          <template #body-cell-state="props">
            <q-td :props="props">
              <q-icon
                v-if="props.row.state === 3"
                name="check_circle"
                color="green"
                size="25px"
              ></q-icon>

              <q-icon
                v-if="props.row.state === 2"
                name="check_circle"
                color="yellow"
                size="25px"
              ></q-icon>
              <q-icon
                v-else-if="props.row.state === 1"
                name="cancel"
                color="red"
                size="25px"
              ></q-icon>
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
          <q-card-section>
            <p><strong>Nombre:</strong> {{ editedRow.name }}</p>
          </q-card-section>
          <q-card-section>
            <p><strong>Apellido:</strong> {{ editedRow.lastName }}</p>
          </q-card-section>
          <q-card-section>
            <p><strong>Placa:</strong> {{ editedRow.placa }}</p>
          </q-card-section>
          <q-select
            v-model="editedRow.state"
            :options="stateOptions"
            label="Estado"
            :value-label="selectedStateLabel"
          />
          {{ console.log(editedRow.state) }}
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
            <p><strong>Nombre:</strong> {{ editedRow.name }}</p>
          </q-card-section>
          <q-card-section>
            <p><strong>Apellido:</strong> {{ editedRow.lastName }}</p>
          </q-card-section>
          <q-card-section>
            <p><strong>Placa:</strong> {{ editedRow.placa }}</p>
          </q-card-section>
          <q-card-section>
            <p><strong>Estado:</strong> {{ selectedStateLabel() }}</p>
          </q-card-section>
        </q-card-section>
        <q-card-actions align="right">
          <q-btn label="Cancelar" color="negative" @click="closeEditDialog" />
          <q-btn label="Eliminar" color="primary" @click="deleteServices" />
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
const columns = [
  {
    name: "state",
    align: "left",
    label: "Estado",
    field: "state",
    sortable: true,
  },
  {
    name: "vehicle",
    align: "left",
    label: "Vehiculo",
    field: "placa",
    sortable: true,
  },
  {
    name: "client",
    align: "center",
    label: "Cliente",
    field: "name",
    sortable: false,
  },
  {
    name: "lastName",
    align: "center",
    label: "Apellido",
    field: "lastName",
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
const stateOptions = [
  { label: "Procesando", value: 2 },
  { label: "Listo para entrega", value: 3 },
  { label: "En espera", value: 1 },
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
const editedRow = ref({ name: "", lastName: "", placa: "", state: "" });

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
      clientId: res.client.clientId,
      name: res.client.name,
      lastName: res.client.lastName,
      vehicleId: res.vehicle.id,
      placa: res.vehicle.placa,
      vehicleLineaId: res.vehicle.vehicleLineaId,
      // Agregar una propiedad para los botones de acciones
      actions: null,
    }));
  } catch (error) {
    console.error("Error al obtener datos:", error);
  } finally {
    loading.value = false;
  }
}

function start() {
  fetchDataWork();
}

const showTable = ref(false);

function toggleTable() {
  showTable.value = !showTable.value;
}

function openEditDialog(row) {
  selectedRow = row;
  editedRow.value = { ...row };
  editDialogVisible.value = true;
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

async function deleteServices() {
  try {
    const url =
      process.env.API_BASE_URL + "/taller/ServiceDetalle?id=" + selectedRow.id;
    await axios.delete(url);
    fetchDataWork();
    closeDeleteDialog();
  } catch (error) {
    console.error("Error al eliminar el trabajo:", error);
  }
}

async function saveChanges() {
  try {
    const url = process.env.API_BASE_URL + "/taller/ServiceDetalle";
    await axios.put(url, {
      id: editedRow.value.id,
      name: editedRow.value.name,
      lastName: editedRow.value.lastName,
      placa: editedRow.value.placa,
      state: editedRow.value.state.value,
      serviceListWorks: [],
      detalleWorkServices: [],
    });
    fetchDataWork();
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
