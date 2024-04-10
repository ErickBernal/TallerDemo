<template>
  <!-- <q-page> -->
  <!-- <div class="row q-col-gutter-sm q-ma-xs"> -->
  <!-- <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12"> -->
  <q-card class="my-card" flat bordered shadow-xl>
    <q-card-section class="bg-teal text-white">
      <div class="text-h5 q-mt-sm q-mb-xs">Estado de servicio</div>
    </q-card-section>
    <q-card-section horizontal>
      <!--  -->
      <q-card-section class="q-pt-xs">
        <div class="text-overline">US Region</div>
        <div class="text-h5 q-mt-sm q-mb-xs">Mayank Patel</div>
        <div class="text-caption text-grey">
          Sales and Marketing Executive | Graduate and past committee | Keynote
          speaker on Selling and Recruiting Topics
        </div>
        <!--  -->

        <!-- <q-select
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
          </q-select> -->

        <!-- Se creo un nuevo modulo FindVehicle que reemplaza a esto comentado -->
        <FindVehicle @whisperedSecret="hearSecret" />
        selected: {{ JSON.stringify(vehicleLineSelected) }}

        <!--  -->
        <!--  -->
      </q-card-section>

      <!--  -->
      <q-card-section class="col-5 flex flex-center">
        <q-img
          class="rounded-borders"
          src="https://blog.continentalmotores.com/hubfs/SOLUCIONWEB/Blog/Posts%20blogs/continental-motores-carros-en-guatemala-volkswagen-blog-un-carro-de-origen-aleman-conoce-sus-ventajas.jpg"
        />
      </q-card-section>
    </q-card-section>

    <q-separator />

    <q-card-section>
      Assessing clients needs and present suitable promoted products. Liaising
      with and persuading targeted doctors to prescribe our products utilizing
      effective sales skills.
    </q-card-section>
  </q-card>
  <!-- </div> -->
  <!-- </div> -->
  <!-- </q-page> -->
</template>
<!-- ------------------------------------------------------------------------------ -->
<script>
import { ref, onMounted } from "vue";
import { LocalStorage } from "quasar";
import axios from "axios";
import { useQuasar } from "quasar";
import FindVehicle from "../vehicleParts/FindVehicle.vue";

export default {
  components: {
    FindVehicle,
  },
  setup() {
    const start = () => {
      LocalStorage.set("owner", false);
    };

    onMounted(start);

    return {};
  },
};
</script>
