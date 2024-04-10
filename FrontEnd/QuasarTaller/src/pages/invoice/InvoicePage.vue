<template>
  <q-page>
    <div class="row q-col-gutter-sm q-ma-xs">
      <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
        <q-card class="my-card" flat bordered shadow-xl>
          <q-card-section class="bg-teal text-white">
            <div class="text-h5 q-mt-sm q-mb-xs">Estado de servicio</div>
          </q-card-section>
          <q-card-section horizontal>
            <q-card-section class="q-pt-xs">
              <div class="text-overline">US Region</div>
              <div class="text-h5 q-mt-sm q-mb-xs">Mayank Patel</div>
              <div class="text-caption text-grey">
                Sales and Marketing Executive | Graduate and past committee |
                Keynote speaker on Selling and Recruiting Topics
              </div>
            </q-card-section>

            <q-card-section class="col-5 flex flex-center">
              <q-img
                class="rounded-borders"
                src="https://blog.continentalmotores.com/hubfs/SOLUCIONWEB/Blog/Posts%20blogs/continental-motores-carros-en-guatemala-volkswagen-blog-un-carro-de-origen-aleman-conoce-sus-ventajas.jpg"
              />
            </q-card-section>
          </q-card-section>

          <q-separator />

          <q-card-section>
            Assessing clients needs and present suitable promoted products.
            Liaising with and persuading targeted doctors to prescribe our
            products utilizing effective sales skills.
          </q-card-section>
        </q-card>
      </div>
      <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
        <q-card>
          <q-card-section class="bg-teal text-white">
            <div class="text-h5 q-mt-sm q-mb-xs">Nuevo Servicio</div>
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
  </q-page>
</template>
<!-- ------------------------------------------------------------------------------ -->
<script setup>
import { ref, onMounted } from "vue";
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

    alert("Servicio agregado correctamente:");
  } catch (error) {
    alert("SError al agregar nuevo servicio");
  }
}
</script>
