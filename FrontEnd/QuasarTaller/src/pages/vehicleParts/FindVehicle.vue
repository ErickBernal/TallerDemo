<template>
  <!--  -->
  <!-- <p>(estoy en child)Datos del Hijo: {{ sendDataParentToChild }}</p> -->
  <!-- <button @click="getDataFromChild">
    (estoy en child)Enviar Datos al Padre
  </button> -->
  <!--  -->

  <q-select
    v-model="vehicleSelected"
    :options="vehicles"
    label="Vehiculo"
    @update:modelValue="getVehicleLineById"
    filled
  >
    <template v-slot:append>
      <span v-if="vehicleLineSelected">
        <!-- {{ getVehicle(vehicleSelected) }} -->
        <div class="text-gray text-h6 q-mt-sm q-mb-xs">
          {{ vehicleLineSelected.line }} {{ vehicleLineSelected.type }}
          {{ vehicleLineSelected.color }}
        </div>
      </span>
      <span v-else> Cargando... </span>
    </template>
  </q-select>
</template>
<!-- ------------------------------------------------------------------------------ -->
<script setup>
import {
  ref,
  onMounted,
  onBeforeMount,
  onBeforeUnmount,
  onUnmounted,
  defineProps,
  defineEmits,
} from "vue";
import axios from "axios";
//----------------------------------------------

//sendDataParentToChild: in FindVehicle.vue , receives the data from StatusServices.vue
const props = defineProps({
  sendDataParentToChild: Object,
});

const emit = defineEmits(["enviarDatosPadre"]);

//send de data to parent
const getDataFromChild = () => {
  emit("enviarDatosPadre", vehicleSelected.value);
};

//----------------------------------------------
const vehicleSelected = ref("");
const vehicles = ref([]);
const vehicleLineSelected = ref([]);
// const props = defineProps(["vehicleSelected2"]);

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
    getDataFromChild();
  } catch (error) {
    console.error("Error al obtener departamentos:", error);
  }
}

onBeforeMount(async () => {
  getVehicles();
});

onUnmounted(() => {
  // Envía la prop a otro componente antes de desmontar
  // emit("getVehicle", vehicleSelected);
});

onMounted(async () => {});
</script>
