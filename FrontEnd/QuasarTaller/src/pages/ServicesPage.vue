<template>
  <q-page paddiing>
    <div class="q-ma-md row justify-center items-center full-height">
      <div class="col-auto">
        <q-btn
          padding="xl"
          color="primary"
          round
          icon="eco"
          label="Nuevo Servicio"
          to="nuevoservicio"
        />
      </div>
      <div class="col-auto q-mx-md">
        <q-btn
          padding="xl"
          round
          color="teal"
          icon="search"
          label="Estado de servicios"
          to="/estatusservicio"
        />
      </div>
    </div>
  </q-page>
</template>

<!-- ///////////////////////////////////////////////////////////////////// -->
<script>
import { ref, onMounted, computed } from "vue";
import { useQuasar } from "quasar";
const $q = useQuasar();

function start() {
  $q.localStorage.set("owner", false);
}
const handleButtonClick = () => {};

onMounted(start);
</script>
