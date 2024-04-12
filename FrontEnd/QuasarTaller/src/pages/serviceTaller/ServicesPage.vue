<template>
  <br />
  <br />
  <q-page padding>
    <div class="row q-col-gutter-sm q-ma-xs">
      <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
        <StatusServices />
      </div>

      <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
        <NewServices />
      </div>
      <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
        <UpdateService />
      </div>
    </div>
    <!-- ----------------------------------------------- -->
  </q-page>
</template>

<!-- ///////////////////////////////////////////////////////////////////// -->
<script>
import { ref, onMounted, computed } from "vue";
import { LocalStorage } from "quasar";
import NewServices from "./NewServices.vue";
import StatusServices from "./StatusServices.vue";
import UpdateService from "./UpdateService.vue";

export default {
  components: {
    NewServices,
    StatusServices,
    UpdateService,
  },
  setup() {
    const start = () => {
      LocalStorage.set("owner", false);
    };

    const handleButtonClick = (buttonNumber) => {
      console.log("Hiciste clic en el botón " + buttonNumber);
    };

    onMounted(start);

    return {
      handleButtonClick,
    };
  },
};
</script>
