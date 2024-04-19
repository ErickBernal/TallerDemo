<template>
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
              v-if="vp !== ''"
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
              v-if="vp !== ''"
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
</template>
<!-- ------------------------------------------------------- -->
<script setup>
import { ref, onBeforeMount, defineModel } from "vue";
import axios from "axios";

const dense = ref(false);
const clientSelected = defineModel("clientSelected");
const clients = ref([]);
const vp = ref(false);
const vehicleSelected = defineModel("vehicleSelected");
const vehicles = ref([]);

//
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

const getLabelClientes = (option) => {
  if (option == "") return "Seleccione Cliente";
  return `${option.name} ${option.lastName}   DPI:${option.dpi} Tel: ${option.phone}`;
};

const getLabelVehicles = (option) => {
  if (option == "") return "Seleccione Vehiculo";
  return `${option.placa}: ${option.vehicleLinea.type} ${option.vehicleLinea.line} `;
};

onBeforeMount(async () => {
  getClients();
  getVehicles();
});
</script>

<style>
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
