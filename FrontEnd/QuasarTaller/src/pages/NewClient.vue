<template>
  <h4 class="justify-center">Nuevo cliente</h4>
  <q-page class="q-pa-md">
    <q-card class="q-mb-md">
      <q-card-section>
        <q-form @submit="submitForm">
          <q-input v-model="newClient.name" label="Nombre" />
          <q-input v-model="newClient.email" label="Correo electrónico" />
          <q-input v-model="newClient.phone" label="Teléfono" />
          <q-select
            v-model="newClient.clientType"
            :options="clientTypes"
            label="Tipo de Cliente"
          />
          <q-card class="q-mb-md">
            <q-card-section>
              Dirección
              <q-form @submit="submitForm">
                <q-select
                  v-model="selectedCountry"
                  :options="countries"
                  label="País"
                  @update:modelValue="updateSelectedCountryId"
                />
                <q-select
                  v-model="selectedDepartment"
                  :options="departments"
                  label="Departamento"
                  @update:modelValue="updateSelectedDepartmentId"
                />
                <q-select
                  v-model="selectedMunicipality"
                  :options="municipalities"
                  label="Municipio"
                />
                <q-input v-model="newClient.address" label="Direccion" />
                <q-input v-model="newClient.zone" label="Zona" />
              </q-form>
            </q-card-section>
          </q-card>
          <q-btn type="submit" label="Agregar Cliente" color="primary" />
        </q-form>
      </q-card-section>
    </q-card>
  </q-page>
</template>

<script setup>
import { ref, onMounted } from "vue";
import axios from "axios";
import { useQuasar } from "quasar";

const $q = useQuasar();

const newClient = ref({
  name: "",
  email: "",
  phone: "",
  clientType: "",
  address: "",
  zone: "",
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
    newAddress.value = {
      MunicipalityId: MunicipalityId.value.id,
      Description: newClient.value.address,
      Zone: newClient.value.zone,
    };
    const response = await axios.post(
      process.env.API_BASE_URL + "/taller/Address",
      newAddress.value
    );

    console.log("Address agregado correctamente:", response.data);
    $q.notify({
      type: "positive",
      message: "Cliente agregado correctamente",
    });
  } catch (error) {
    console.error("Error al agregar cliente:", error);
    $q.notify({
      type: "negative",
      message: "Error al agregar cliente",
    });
  }
};

// Método para actualizar el ID del Departamento seleccionado
const updateSelectedDepartmentId = async (value) => {
  // console.log(
  //   "++++> " +
  //     selectedDepartment.value.id +
  //     " _..._ " +
  //     selectedDepartment.value.label +
  //     " _..._ " +
  //     selectedCountry.value.label
  // );
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
