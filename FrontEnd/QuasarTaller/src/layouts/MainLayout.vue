<template>
  <!-- https://quasar.dev/layout/layout -->
  <q-layout view="hHh lpR fFf">
    <q-header elevated class="bg-primary text-white" height-hint="98">
      <q-toolbar>
        <q-toolbar-title>
          <q-avatar class="bg-white">
            <img src="~assets/logo.png" />
          </q-avatar>
          Taller
        </q-toolbar-title>
        <q-space />
        {{ localStorage.getItem("bannerName") }}
        <q-btn flat round dense icon="more_vert" />
      </q-toolbar>

      <q-tabs align="left">
        <q-route-tab @click="openPasswordModal" label="Log" />

        <!--  -->
        <q-btn-dropdown auto-close stretch flat icon="build" label="Repuestos">
          <q-list>
            <q-item clickable to="/buscarrepuesto" @click="amInVehicleParts">
              <q-item-section>Buscar repuesto</q-item-section>
            </q-item>

            <q-item clickable to="/nuevorepuesto" @click="amInVehicleParts">
              <q-item-section>Crear repuesto</q-item-section>
            </q-item>
          </q-list>
        </q-btn-dropdown>
        <!--  -->
        <q-route-tab to="/servicios" label="Servicios" @click="amInServices" />
        <q-route-tab label="Cliente" />
        <q-route-tab label="Factura" />
        <!--  -->

        <!--  -->
      </q-tabs>
    </q-header>

    <q-page-container>
      <router-view />
    </q-page-container>

    <!-- Modal para ingresar la contraseña -->
    <q-dialog v-model="passwordModalVisible">
      <q-card>
        <q-card-section>
          <q-input v-model="password" type="password" label="Contraseña" />
        </q-card-section>
        <q-card-actions align="right">
          <q-btn label="Cancelar" color="red" @click="closePasswordModal" />
          <q-btn label="Aceptar" color="primary" @click="submitPassword" />
        </q-card-actions>
      </q-card>
    </q-dialog>
  </q-layout>
</template>

<script setup>
import { ref } from "vue";
import axios from "axios";
import { useRoute, useRouter } from "vue-router";
import { useQuasar } from "quasar";

const { localStorage } = useQuasar();
const passwordModalVisible = ref(false);
const password = ref("");
const router = useRouter();
const amIn = ref("");

const openPasswordModal = () => {
  password.value = "";
  passwordModalVisible.value = true;
};

const closePasswordModal = () => {
  passwordModalVisible.value = false;
  // Redirige al usuario a la página principal ("/") al cancelar
};

const url = process.env.API_BASE_URL + `/taller/Owner`;

const submitPassword = async () => {
  try {
    const response = await axios.post(url, {
      pwd: password.value,
    });

    const isValid = response.data; // Suponiendo que el endpoint devuelve un objeto con un campo isValid indicando si la contraseña es válida o no
    if (isValid) {
      // Contraseña válida, redirige al usuario a la página de log
      closePasswordModal();
      // Redirige a la página de log
      localStorage.set("owner", true);
      // console.log("localStorage: " + localStorage.getItem(1));
      router.push("/log");
      localStorage.set("bannerName", "Logs");
    } else {
      // Contraseña inválida, muestra un mensaje de error
      console.error("Contraseña inválida");
      // Aquí puedes mostrar un mensaje de error al usuario
    }
  } catch (error) {
    // console.error("Error al validar la contraseña", error);
    alert("Contraseña incorrecta");
    router.push("/");
    localStorage.set("bannerName", "");

    // Aquí puedes manejar el error de la llamada al endpoint
  }
};

const amInVehicleParts = () => {
  localStorage.set("bannerName", "Repuestos");
};
const amInServices = () => {
  localStorage.set("bannerName", "Servicios");
};

defineOptions({
  name: "MainLayout",
});
</script>
