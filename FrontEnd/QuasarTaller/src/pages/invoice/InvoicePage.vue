<template>
  <!--  -->

  <q-card
    class="horizontal-card"
    flat
    bordered
    shadow-xl
    :class="{ 'h-50': !dense, 'h-100': dense }"
  >
    <!-- <q-card-section class="horizontal-section bg-teal text-white   "> -->
    <q-card-section class="horizontal-section bg-teal text-white">
      <q-card-section horizontal>
        <!-- <div class="text-h6 q-mt-sm q-mb-xs text-white">Cliente:</div> -->
        <q-select
          filled
          v-model="clientSelected"
          :options="clients"
          :option-value="'id'"
          :option-label="getLabelClientes"
          :dense="dense"
          bg-color="white"
        >
          <template v-slot:before>
            <q-avatar>
              <img src="https://cdn.quasar.dev/img/avatar5.jpg" />
            </q-avatar>
          </template>

          <template v-slot:append>
            <q-icon
              v-if="model !== ''"
              name="close"
              @click.stop.prevent="clientSelected = ''"
              class="cursor-pointer"
            ></q-icon>
          </template>
        </q-select>
      </q-card-section>
      <!--  -->
      <q-card-section horizontal>
        <!-- <div class="text-h6 q-mt-sm q-mb-xs">Vehiculo:</div> -->
        <q-select
          filled
          v-model="vehicleSelected"
          :options="vehicles"
          :option-value="'id'"
          :option-label="getLabelVehicles"
          :dense="dense"
          bg-color="white"
        >
          <template v-slot:before>
            <q-avatar>
              <img
                src="https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRVakPl6weDWAvYrqQFM-LndbL5-JIJgVH7meM0Uo3RjgeI3DgcuqYJhewVn6GKgu0QwBo&usqp=CAU"
              />
            </q-avatar>
          </template>

          <template v-slot:append>
            <q-icon
              v-if="model !== ''"
              name="close"
              @click.stop.prevent="vehicleSelected = ''"
              class="cursor-pointer"
            ></q-icon>
          </template>
        </q-select>
      </q-card-section>
      <div class="q-pb-lg">
        <q-toggle v-model="dense" label="Dense" />
      </div>
    </q-card-section>
  </q-card>
  <!--  -->

  <div class="row q-col-gutter-sm q-ma-xs">
    <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
      <q-card class="my-card" flat bordered shadow-xl>
        <q-card-section class="bg-teal text-white">
          <div class="text-h5 q-mt-sm q-mb-xs">Repuestos utilizados</div>
        </q-card-section>
        <q-card-section>
          <q-table
            flat
            bordered
            title="Repuestos"
            :rows="VehicleParts"
            :columns="columnsVehicleParts"
            row-key="id"
            :loading="loading"
          >
            <template #body-cell-Cant="props">
              <q-td :props="props">
                <q-input
                  v-model="props.row.detalleVehicleParts"
                  label="Cantidad"
                  filled
                  type="number"
                  mask="int"
                  lazy-rules
                  :rules="[
                    (val) => (val && val.length > 0) || 'Ingrese cantidad',
                  ]"
                />
              </q-td>
            </template>
          </q-table>
          <q-btn icon="delete" @click="prueba()" />
        </q-card-section>
      </q-card>
    </div>
    <!--  -->
    <!--  -->
    <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
      <q-card>
        <q-card-section class="bg-teal text-white">
          <div class="text-h5 q-mt-sm q-mb-xs">Servicios implementados</div>
        </q-card-section>
        <!--  -->
        <q-card class="q-mb-md">
          <q-card-section>
            <q-form @submit="submitForm" class="q-gutter-md">
              <q-input
                v-model="newServices.km"
                label="Kilometraje del vehiculo"
                type="number"
                mask="int"
                filled
              />

              <q-select
                v-model="newServices.servicesType"
                :options="servicesType"
                label="Tipo de cliente"
                filled
              />
              <q-select
                v-model="newServices.vehicle"
                :options="vehicles"
                label="Vehiculo"
                @update:modelValue="getVehicleLineById"
                filled
              >
                <template v-slot:append>
                  <span v-if="vehicleLine">
                    {{ vehicleLine.line }} {{ vehicleLine.type }}
                    {{ vehicleLine.color }}
                  </span>
                  <span v-else> Cargando... </span>
                </template>
              </q-select>

              <q-btn type="submit" label="Crear servicio" color="primary" />
            </q-form>
          </q-card-section>
        </q-card>
        <!--  -->
      </q-card>
    </div>
  </div>
</template>
<!-- ------------------------------------------------------------------------------ -->
<script setup>
import { ref, onMounted, onBeforeMount } from "vue";
import axios from "axios";
import { useQuasar } from "quasar";

const newServices = ref({
  vehicle: "",
  servicesType: "",
  km: "",
});

const vehicles = ref([]);
const servicesType = ref([]);
const vehicleLine = ref([]); // Inicialmente establecido como null
const dense = ref(false);
const clients = ref([]);
const clientSelected = ref([]);
const vehicleSelected = ref([]);
const selectedTableRow = ref([]);
const VehicleParts = ref([]);
const VehiclePartSelected = ref([]);
const serviceWork = ref([]);
const ServiceDetalle = ref([]);
const loading = ref(false);

const columnsVehicleParts = [
  {
    name: "name",
    align: "center",
    label: "Nombre",
    field: "name",
    sortable: true,
  },
  {
    name: "stock",
    align: "center",
    label: "Stock",
    field: "stock",
    sortable: true,
  },
  {
    name: "unitprice",
    align: "center",
    label: "(Q) Precio unitario",
    field: "unitPrice",
    sortable: true,
  },
  {
    name: "Cant",
    align: "center",
    label: "Cant.compra",
    field: "Cant",
    sortable: true,
  },
];
const columnsServicioDetalle = [
  {
    name: "name",
    align: "center",
    label: "Nombre",
    field: "name",
    sortable: true,
  },
  {
    name: "stock",
    align: "center",
    label: "Stock",
    field: "stock",
    sortable: true,
  },
  {
    name: "unitprice",
    align: "center",
    label: "(Q) Precio unitario",
    field: "unitPrice",
    sortable: true,
  },
  {
    name: "Cant",
    align: "center",
    label: "Cant.compra",
    field: "Cant",
    sortable: true,
  },
];
const columnsWorks = [
  {
    name: "name",
    align: "center",
    label: "Nombre",
    field: "name",
    sortable: true,
  },
  {
    name: "stock",
    align: "center",
    label: "Stock",
    field: "stock",
    sortable: true,
  },
  {
    name: "unitprice",
    align: "center",
    label: "(Q) Precio unitario",
    field: "unitPrice",
    sortable: true,
  },
  {
    name: "Cant",
    align: "center",
    label: "Cant.compra",
    field: "Cant",
    sortable: true,
  },
];

const getLabelClientes = (option) => {
  if (option == "") return "Seleccione Cliente";
  return `${option.name} ${option.lastName}   DPI:${option.dpi} Tel: ${option.phone}`;
};

const getLabelVehicles = (option) => {
  if (option == "") return "Seleccione Vehiculo";
  return `${option.placa}: ${option.vehicleLinea.type} ${option.vehicleLinea.line} `;
};

async function getClients() {
  try {
    const response = await axios.get(
      process.env.API_BASE_URL + "/taller/Client"
    );
    clients.value = response.data;
  } catch (error) {
    alert("Error al obtener Clientes");
  }
}

async function getVehicles() {
  try {
    const response = await axios.get(
      process.env.API_BASE_URL + "/taller/Vehicle"
    );
    vehicles.value = response.data;
  } catch (error) {
    alert("Error al obtener Vehiculos");
  }
}

async function getVehicleParts() {
  loading.value = true;

  try {
    const response = await axios.get(
      process.env.API_BASE_URL + "/taller/VehicleParts"
    );
    VehicleParts.value = response.data;
  } catch (error) {
    alert("Error al obtener Vehiculos");
  }
  loading.value = false;
}

async function getVehicleLineById(value) {
  try {
    const response = await axios.get(
      process.env.API_BASE_URL +
        "/taller/VehicleLine/" +
        newServices.value.vehicle.vehicleLineaId
    );
    const res = response.data;
    vehicleLine.value = {
      color: res.color,
      type: res.type,
      line: res.line,
    };
  } catch (error) {
    console.error("Error al obtener departamentos:", error);
  }
}

async function submitForm() {
  try {
    const sendData = {
      state: 1,
      km: newServices.value.km,
      serviceTypeId: newServices.value.servicesType.id,
      vehicleId: newServices.value.vehicle.id,
    };
    const response = await axios.post(
      process.env.API_BASE_URL + "/taller/ServiceDetalle",
      sendData
    );

    alert("Servicio agregado correctamente:");
  } catch (error) {
    alert("SError al agregar nuevo servicio");
  }
}

async function getServiceDetalle() {
  try {
    const response = await axios.get(
      process.env.API_BASE_URL + "/taller/ServiceDetalle"
    );
    serviceWork.value = response.data;
  } catch (error) {
    alert("Error al obtener ServiceWork");
  }
}

async function getServicesWork() {
  try {
    const response = await axios.get(
      process.env.API_BASE_URL + "/taller/ServiceWork"
    );
    serviceWork.value = response.data;
  } catch (error) {
    alert("Error al obtener ServiceWork");
  }
}

function prueba() {
  console.log("-.-.-->" + JSON.stringify(serviceWork.value));
  // console.log("----******-->" + JSON.stringify(vehicles.value));
}

onBeforeMount(async () => {
  getClients();
  getVehicles();
  getVehicleParts();
  getServicesWork();
  getServiceDetalle();
});
</script>

<style>
.horizontal-card {
  display: flex;
  flex-direction: row;
  justify-content: center; /* Para centrar los elementos horizontalmente */
}

.horizontal-section {
  display: flex;
  flex-direction: row;
  justify-content: space-between; /* Para distribuir los elementos horizontalmente */
  width: 100%; /* Opcional, para asegurarse de que los elementos ocupen todo el ancho disponible */
}
.w-50 {
  width: 50%;
}

.w-100 {
  width: 100%;
}
.h-50 {
  height: 30%;
}

.h-100 {
  height: 70%;
}
.white-label .q-field__label {
  color: white;
}
</style>
