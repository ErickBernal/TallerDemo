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
          <!-- Columna para botones Update -->
          <template #body-cell-Update="props">
            <q-td :props="props">
              <q-btn icon="save" @click="openEditDialog(props.row)" />
            </q-td>
          </template>
          <!-- Columna para botones Delete -->
          <template #body-cell-Delete="props">
            <q-td :props="props">
              <q-btn icon="delete" @click="openDeleteDialog(props.row)" />
            </q-td>
          </template>
          <!-- Edited row -->
          <template v-slot:body-cell="props">
            <q-td :props="props">
              <q-input
                v-model="props.row[props.col.name]"
                input-class="text-center"
                dense
                borderless
              />
            </q-td>
          </template>
          <!--  -->
          <template #body-cell-typeClient="props">
            <q-td :props="props">
              <div class="q-gutter-xl">
                <q-select
                  color="orange"
                  filled
                  v-model="props.row[props.col.name]"
                  :options="optionsTypeClient"
                  :option-value="'id'"
                  :option-label="'type'"
                  label="Tipo de cliente"
                />
              </div>
            </q-td>
          </template>
          <!-- DropDown button  -->
          <template #body-cell-DIR="props">
            <q-td :props="props">
              <q-btn icon="map" @click="openAddressDialog(props.row)" />
            </q-td>
          </template>
          <!--  -->
        </q-table>
      </q-card-section>
    </q-card>

    <!--  -->
    <q-dialog v-model="deleteDialogVisible">
      <q-card>
        <q-card-section class="bg-grey-8 text-white">
          <div class="text-h5 q-mt-sm q-mb-xs">Eliminar usuario</div>
        </q-card-section>
        <q-card-section>
          <q-card-section>
            <p><strong>Nombre:</strong> {{ editedRow.name }}</p>
          </q-card-section>
          <q-card-section>
            <p><strong>Apellido:</strong> {{ editedRow.lastName }}</p>
          </q-card-section>
          <q-card-section>
            <p><strong>Dpi:</strong> {{ editedRow.dpi }}</p>
          </q-card-section>
        </q-card-section>
        <q-card-actions align="right">
          <q-btn label="Cancelar" color="negative" @click="closeDeleteDialog" />
          <q-btn label="Eliminar" color="primary" @click="deleteClient" />
        </q-card-actions>
      </q-card>
    </q-dialog>
    <!--  -->
    <q-dialog v-model="editDialogVisible">
      <q-card>
        <q-card-section
          class="bg-teal text-white"
          style="
            display: flex;
            justify-content: space-between;
            align-items: center;
          "
        >
          <div class="text-h5 q-mt-sm q-mb-xs">Actualizar usuario</div>
        </q-card-section>
        <!--  -->
        <q-card-section>
          <q-card-section>
            <q-badge color="secondary" multi-line> Datos </q-badge>

            <p><strong>Nombre:</strong> {{ editedRow.name }}</p>
            <p><strong>Apellido:</strong> {{ editedRow.lastName }}</p>
            <p><strong>Dpi:</strong> {{ editedRow.dpi }}</p>
            <p><strong>Telefono:</strong> {{ editedRow.phone }}</p>
            <p><strong>Celular:</strong> {{ editedRow.cellphone }}</p>
            <p>
              <strong>Tipo de cliente:</strong> {{ editedRow.typeClient.type }}
            </p>
            <!-- 
            <q-badge color="secondary" multi-line> Direccion </q-badge>
            <p>
              <strong>Pais:</strong>
              {{ editedRow.municipality.department.country.name }}
            </p>
            <p>
              <strong>Departamento:</strong>
              {{ editedRow.municipality.department.name }}
            </p>
            <p>
              <strong>Municipalidad:</strong>
              {{ editedRow.municipality.name }}
            </p>
            <p>
              <strong>Direccion:</strong>
              {{ editedRow.address }}
            </p>
            <p>
              <strong>Zona:</strong>
              {{ editedRow.zone }}
            </p> -->
          </q-card-section>
        </q-card-section>
        <!--  -->
        <q-card-actions align="right">
          <q-btn label="Cancelar" color="negative" @click="closeEditDialog" />
          <q-btn label="Actualizar" color="primary" @click="saveChanges" />
        </q-card-actions>
      </q-card>
    </q-dialog>
    <!--Create new user  -->
    <q-dialog v-model="createDialogVisible">
      <NewClient componentName="Create nuevo usuario" />
    </q-dialog>
    <!-- Address  -->
    <q-dialog v-model="addressVisible">
      <AddressComponent
        v-model:selectedCountry="selectedCountry"
        v-model:selectedDepartment="selectedDepartment"
        v-model:selectedMunicipality="selectedMunicipality"
        v-model:selectedAddress="selectedAddress"
        v-model:selectedZone="selectedZone"
        v-model:activateBtn="activateBtn"
      />
    </q-dialog>

    <!--  -->
  </q-card>
</template>

<script setup>
import { ref, onMounted, computed } from "vue";
import axios from "axios";
import AddressComponent from "./AddressComponent.vue";
import NewClient from "./NewClient.vue";

const loading = ref(false);
const rows = ref([]);
const onlyOneGet = ref(true);
const filterText = ref("");
let selectedRow = null;
const editDialogVisible = ref(false);
const deleteDialogVisible = ref(false);
const createDialogVisible = ref(false);
const addressVisible = ref(false);
//new props
const selectedCountry = ref("");
const selectedDepartment = ref("");
const selectedMunicipality = ref("");
const selectedAddress = ref("");
const selectedZone = ref("");
const editedRow = ref({});
const activateBtn = ref(true);

const columns = [
  {
    name: "name",
    align: "center",
    label: "Nombre",
    field: "name",
    sortable: true,
  },
  {
    name: "lastName",
    align: "center",
    label: "Apellido",
    field: "lastName",
    sortable: true,
  },
  {
    name: "dpi",
    align: "center",
    label: "DPI",
    field: "dpi",
    sortable: false,
  },
  {
    name: "phone",
    align: "center",
    label: "Telefono",
    field: "phone",
    sortable: false,
  },

  {
    name: "cellphone",
    align: "center",
    label: "Celular",
    field: "cellphone",
    sortable: false,
  },
  {
    name: "typeClient",
    align: "center",
    label: "Tipo de cliente",
    field: "typeClient",
    sortable: false,
  },
  {
    name: "DIR",
    align: "center",
    label: "Direccion",
    field: "ddress",
    sortable: false,
  },
  {
    name: "Update",
    align: "center",
    label: "Editar",
    field: "Update",
    sortable: false,
  },
  {
    name: "Delete",
    align: "center",
    label: "Eliminar",
    field: "Delete",
    sortable: false,
  },
];

const filteredRows = computed(() => {
  return rows.value.filter(
    (row) =>
      row.name.toLowerCase().includes(filterText.value.toLowerCase()) ||
      row.lastName
        .toString()
        .toLowerCase()
        .includes(filterText.value.toLowerCase()) ||
      row.dpi.toString().toLowerCase().includes(filterText.value.toLowerCase())
  );
});

const optionsTypeClient = ref([
  { id: 1, type: "Individual", clients: null },
  { id: 2, type: "Empresa", clients: null },
]);

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

function openCreateUser() {
  createDialogVisible.value = true;
}

function openAddressDialog(row) {
  addressVisible.value = true;
  selectedRow = row;
  selectedCountry.value = row.municipality.department.country;
  selectedDepartment.value = row.municipality.department;
  selectedMunicipality.value = row.municipality;
  selectedAddress.value = row.address;
  selectedZone.value = row.zone;
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

async function deleteClient() {
  try {
    const url =
      process.env.API_BASE_URL + "/taller/Client?id=" + selectedRow.id;
    await axios.delete(url);
    getClients();
    closeDeleteDialog();
  } catch (error) {
    console.error("Error al eliminar vehiculo:", error);
    alert("Error al eliminar vehiculo:");
  }
}

async function saveChanges() {
  alert("Seguro que quiere actualizar los datos de: " + selectedRow.name);
  try {
    const url = process.env.API_BASE_URL + "/taller/Client";
    await axios.put(url, {
      id: editedRow.value.id,
      name: editedRow.value.name,
      lastname: editedRow.value.lastName,
      dpi: editedRow.value.dpi,
      nit: editedRow.value.nit,
      phone: editedRow.value.phone,
      cellphone: editedRow.value.cellphone,
      typeclientid: editedRow.value.typeClientId,
      zone: editedRow.value.zone,
      address: editedRow.value.address,
      municipalityid: editedRow.value.municipalityId,
    });
    getClients();
    closeEditDialog();
  } catch (error) {
    console.error("Error al guardar los cambios:", error);
  }
}

function start() {
  getClients();
}

onMounted(start);
</script>
