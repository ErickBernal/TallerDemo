<template>
  <!-- <q-page class="q-pa-md"> -->
  <q-card class="my-card" flat bordered shadow-xl>
    <!--  -->
    <q-card-section>
      <q-card-section class="bg-teal text-white">
        <div class="text-h5 q-mt-sm q-mb-xs">Nuevo clientes</div>
      </q-card-section>
      <br />
      <q-form @submit="submitForm" class="q-gutter-md">
        <q-input
          v-model="newClient.name"
          label="Nombre"
          filled
          type="string"
          mask="string"
          lazy-rules
          :rules="[(val) => (val && val.length > 0) || 'Ingrese un Nombre']"
        />
        <q-input
          v-model="newClient.lastName"
          label="Apellido"
          filled
          type="string"
          mask="string"
          lazy-rules
          :rules="[(val) => (val && val.length > 0) || 'Ingrese un Apellido']"
        />
        <q-input
          v-model="newClient.dpi"
          label="DPI"
          filled
          type="number"
          mask="int"
          lazy-rules
          :rules="[(val) => (val && val.length > 0) || 'Ingrese un DPI valido']"
        />
        <q-input
          v-model="newClient.nit"
          label="NIT"
          filled
          type="number"
          mask="int"
          lazy-rules
          :rules="[(val) => (val && val.length > 0) || 'Ingrese un NIT valido']"
        />
        <q-input
          v-model="newClient.phone"
          label="Teléfono"
          filled
          type="number"
          mask="int"
          lazy-rules
          :rules="[
            (val) =>
              (val && val.length == 8) ||
              'Ingrese un numero de telefono valido',
          ]"
        />
        <q-input
          v-model="newClient.cellphone"
          label="Celular"
          filled
          type="number"
          mask="int"
          lazy-rules
          :rules="[
            (val) =>
              (val && val.length == 8) ||
              'Ingrese un numero de telefono valido',
          ]"
        />

        <q-select
          v-model="newClient.TypeClientId"
          :options="clientTypes"
          label="Tipo de Cliente"
          filled
        />
        <br />

        <q-card class="q-mb-md">
          <q-card-section>
            <q-card-section class="bg-grey-8 text-white">
              <div class="text-h5 q-mt-sm q-mb-xs">Direccion</div>
            </q-card-section>
            <br />
            <q-form @submit="submitForm" class="q-gutter-md">
              <q-select
                v-model="selectedCountry"
                :options="countries"
                label="País"
                @update:modelValue="updateSelectedCountryId"
                filled
              />
              <q-select
                v-model="selectedDepartment"
                :options="departments"
                label="Departamento"
                @update:modelValue="updateSelectedDepartmentId"
                filled
              />
              <q-select
                v-model="selectedMunicipality"
                :options="municipalities"
                label="Municipio"
                filled
              />
              <q-input
                v-model="newClient.address"
                label="Direccion"
                filled
                type="string"
                mask="string"
                lazy-rules
                :rules="[
                  (val) =>
                    (val && val.length > 0) || 'Ingrese una direccion valida',
                ]"
              />
              <q-input
                v-model="newClient.zone"
                label="Zona"
                filled
                type="number"
                mask="int"
                lazy-rules
                :rules="[
                  (val) => (val && val.length > 0) || 'Ingrese una zona valida',
                ]"
              />
            </q-form>
            <q-form @submit="submitForm" class="q-gutter-md"> </q-form>
          </q-card-section>
        </q-card>

        <q-btn type="submit" label="Agregar Cliente" color="primary" />
      </q-form>
    </q-card-section>
  </q-card>
  <!-- </q-page> -->
</template>

<script setup>
import { ref, onMounted } from "vue";
import axios from "axios";
import { useQuasar } from "quasar";

const { cookies, fullscreen } = useQuasar();

const newClient = ref({
  MunicipalityId: "",
  Zone: "",
  name: "",
  lastName: "",
  dpi: "",
  nit: "",
  phone: "",
  cellphone: "",
  TypeClientId: "",
  zone: "",
  address: "",
  MunicipalityId: "",
});

const clientTypes = ref([]);
const countries = ref([]);
const departments = ref([]);
const municipalities = ref([]);
const selectedAddress = ref("");
const selectedCountry = ref("");
const selectedDepartment = ref("");
const selectedMunicipality = ref("");
const newAddress = ref("");

onMounted(async () => {
  // Obtener tipos de cliente
  try {
    const response = await axios.get(
      process.env.API_BASE_URL + "/taller/TypeClient"
    );
    // Mapear la respuesta para obtener los tipos de cliente
    clientTypes.value = response.data.map((type) => ({
      label: type.type,
      id: type.id,
    }));
  } catch (error) {
    console.error("Error al obtener tipos de cliente:", error);
  }

  // Obtener lista de países
  try {
    const response = await axios.get(
      process.env.API_BASE_URL + "/taller/Country"
    );
    countries.value = response.data.map((res) => ({
      label: res.name,
      id: res.id,
    }));
  } catch (error) {
    console.error("Error al obtener países:", error);
  }
});

const submitForm = async () => {
  try {
    const response = await axios.post(
      process.env.API_BASE_URL + "/taller/Client",
      {
        name: newClient.value.name,
        lastname: newClient.value.lastName,
        dpi: newClient.value.dpi,
        nit: newClient.value.nit,
        phone: newClient.value.phone,
        cellphone: newClient.value.cellphone,
        typeClientId: newClient.value.TypeClientId.id,
        zone: newClient.value.zone,
        address: newClient.value.address,
        municipalityId: selectedMunicipality.value.id,
      }
    );

    console.log("Address agregado correctamente:", response.data);
  } catch (error) {
    console.error("Error al agregar cliente:", error);
  }
};

// Método para actualizar el ID del Departamento seleccionado
const updateSelectedDepartmentId = async (value) => {
  selectedMunicipality.value = "";
  selectedDepartment.value = value;
  try {
    const response = await axios.get(
      process.env.API_BASE_URL +
        "/taller/Municipalities/" +
        selectedDepartment.value.id
    );
    municipalities.value = response.data.map((res) => ({
      label: res.name,
      id: res.id,
    }));
  } catch (error) {
    console.error("Error al obtener departamentos:", error);
  }
};

// Método para actualizar el ID del país seleccionado
const updateSelectedCountryId = async (value) => {
  selectedDepartment.value = "";
  selectedMunicipality.value = "";
  selectedCountry.value = value;
  // Obtener lista de departamentos
  try {
    const response = await axios.get(
      process.env.API_BASE_URL +
        "/taller/Department/" +
        selectedCountry.value.id
    );
    departments.value = response.data.map((res) => ({
      label: res.name,
      id: res.id,
    }));
  } catch (error) {
    console.error("Error al obtener departamentos:", error);
  }
};
</script>
