<template>
  <br />
  <br />
  <q-page padding>
    <!-- <div class="q-ma-md row justify-center items-center full-height">
      <div class="row justify-center">
        <div class="col-auto q-mx-md">
          <q-btn
            padding="xl"
            color="primary"
            square
            icon="eco"
            label="Nuevo Servicio"
            to="nuevoservicio"
            @click="handleButtonClick(1)"
          />
        </div>
      </div>
      <div class="row justify-center q-my-md">
        <div class="col-auto q-mx-md">
          <q-btn
            padding="xl"
            square
            color="teal"
            icon="search"
            label="Estado de servicios"
            to="/estatusservicio"
            @click="handleButtonClick(2)"
          />
        </div>
      </div>

      <div class="row justify-center q-my-md">
        <div class="col-auto q-mx-md">
          <q-btn
            padding="xl"
            square
            color="brown"
            icon="search"
            label="Agregar Trabajo"
            to="/agregartrabajo"
            @click="handleButtonClick(4)"
          />
        </div>
      </div>
    </div> -->
    <!-- ---------------------------------------------  -->

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
