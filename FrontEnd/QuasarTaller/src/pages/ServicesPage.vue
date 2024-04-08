<template>
  <br />
  <br />
  <q-page padding>
    <div class="q-ma-md row justify-center items-center full-height">
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
            color="orange"
            icon="search"
            label="Nuevo Cliente"
            to="/nuevocliente"
            @click="handleButtonClick(3)"
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

      <div class="row justify-center q-my-md">
        <div class="col-auto q-mx-md">
          <q-btn
            padding="xl"
            square
            color="black"
            icon="search"
            label="Nuevo vehiculo"
            to="/nuevovehiculo"
            @click="handleButtonClick(4)"
          />
        </div>
      </div>
    </div>
  </q-page>
</template>

<!-- ///////////////////////////////////////////////////////////////////// -->
<script>
import { ref, onMounted, computed } from "vue";
import { useQuasar } from "quasar";

export default {
  setup() {
    const $q = useQuasar();

    const start = () => {
      $q.localStorage.set("owner", false);
    };

    const handleButtonClick = (buttonNumber) => {
      // Aquí puedes definir la lógica que quieras ejecutar al hacer clic en cada botón
      console.log("Hiciste clic en el botón " + buttonNumber);
    };

    onMounted(start);

    return {
      handleButtonClick,
    };
  },
};
</script>
