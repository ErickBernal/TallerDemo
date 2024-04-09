<template>
  <h4 class="justify-center">Nuevo servicio</h4>
  <q-page class="q-pa-md">
    <q-card class="q-mb-md">
      <q-card-section>
        <q-form @submit="submitForm">
          <q-input
            v-model="newServices.km"
            label="Kilometraje del vehiculo"
            type="number"
            mask="int"
          />

          <q-select
            v-model="newServices.servicesType"
            :options="servicesType"
            label="Tipo de cliente"
          />
          <q-select
            v-model="newServices.vehicle"
            :options="vehicles"
            label="Vehiculo"
            @update:modelValue="getVehicleLineById"
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
  </q-page>
</template>

<script setup>
import { ref, onMounted } from "vue";
import axios from "axios";
import { useQuasar } from "quasar";

const { notify } = useQuasar();

const newServices = ref({
  vehicle: "",
  servicesType: "",
  km: "",
});

const vehicles = ref([]);
const servicesType = ref([]);
const vehicleLine = ref([]); // Inicialmente establecido como null

onMounted(async () => {
  // Obtener tipos de cliente y tipos de servicio
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

    const servicesResponse = await axios.get(
      process.env.API_BASE_URL + "/taller/ServiceType"
    );
    servicesType.value = servicesResponse.data.map((s) => ({
      label: s.type,
      id: s.id,
      type: s.type,
      serviceListWorks: s.serviceListWorks,
      serviceDetalles: s.serviceDetalles,
    }));
  } catch (error) {
    console.error("Error al obtener datos:", error);
  }
});

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

    console.log("Servicio agregado correctamente:", response.data);
    $q.notify({
      type: "positive",
      message: "Cliente agregado correctamente",
    });
  } catch (error) {
    console.error("Error al agregar nuevo servicio:", error);
    $q.notify({
      type: "negative",
      message: "Error al agregar cliente",
    });
  }
}
</script>
