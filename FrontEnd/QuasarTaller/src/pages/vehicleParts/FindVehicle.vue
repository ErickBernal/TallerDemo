<template>
  <q-select
    v-model="vehicleSelected"
    :options="vehicles"
    label="Vehiculo"
    @update:modelValue="getVehicleLineById"
    @click="$emit('sendDataChild')"
    filled
  >
    <template v-slot:append>
      <span v-if="vehicleLineSelected">
        {{ vehicleLineSelected.line }} {{ vehicleLineSelected.type }}
        {{ vehicleLineSelected.color }}
      </span>
      <span v-else> Cargando... </span>
    </template>
  </q-select>
</template>
<!-- ------------------------------------------------------------------------------ -->
<script setup>
import { ref, onMounted, onBeforeMount } from "vue";
import axios from "axios";

const vehicleSelected = ref("");
const vehicles = ref([]);
const vehicleLineSelected = ref([]);

async function getVehicles() {
  try {
    const vehiclesResponse = await axios.get(
      process.env.API_BASE_URL + "/taller/Vehicle"
    );
    vehicles.value = vehiclesResponse.data.map((v) => ({
      label: v.placa,
      placa: v.placa,
      id: v.id,
      vehicleLineaId: v.vehicleLineaId,
    }));
  } catch (error) {
    console.error("Error al obtener datos:", error);
    alert("Error al cargar vehiculos.");
  }
}

async function getVehicleLineById() {
  try {
    const response = await axios.get(
      process.env.API_BASE_URL +
        "/taller/VehicleLine/" +
        vehicleSelected.value.vehicleLineaId
    );
    const res = response.data;
    vehicleLineSelected.value = {
      color: res.color,
      type: res.type,
      line: res.line,
    };
  } catch (error) {
    console.error("Error al obtener departamentos:", error);
  }
}

const emit = defineEmits(["sendDataChild"]);

onBeforeMount(async () => {
  getVehicles();
});

onMounted(async () => {});
</script>
