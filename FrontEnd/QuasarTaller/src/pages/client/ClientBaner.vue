<template>
  <q-page padding>
    <div class="row q-col-gutter-sm q-ma-xs">
      <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
        <NewClient />
      </div>
    </div>
  </q-page>
</template>

<!-- //////////////////////////////   ////////////////////////////////////// -->
<script setup>
import { ref, onMounted, computed } from "vue";
import { useQuasar, LocalStorage } from "quasar";
import NewClient from "./NewClient.vue";
import NewVehicle from "../vehicle/NewVehicle.vue";

const start = () => {
  LocalStorage.set("owner", false);
};

const handleButtonClick = (buttonNumber) => {
  // Aquí puedes definir la lógica que quieras ejecutar al hacer clic en cada botón
  console.log("Hiciste clic en el botón " + buttonNumber);
};
onMounted(start);
</script>
