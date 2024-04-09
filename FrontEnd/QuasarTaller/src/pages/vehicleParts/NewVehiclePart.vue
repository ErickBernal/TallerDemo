<template>
  <div class="q-pa-md row items-start q-gutter-md">
    <!--  -->
    <q-dialog v-model="isActiveNewVehicle">
      <q-card class="my-card">
        <q-card class="my-card">
          <q-card-section class="bg-teal text-white">
            <div class="text-h6">Crear Repuesto</div>
            <!-- <div class="text-subtitle2">by John Doe666</div> -->
          </q-card-section>
          <!--  -->
          <q-card class="q-mb-md">
            <q-card class="q-mb-md">
              <q-card-section>
                <div class="q-gutter-md">
                  <q-form @submit="saveVehiclePart">
                    <q-input
                      v-model="newVehiclePat.name"
                      label="Nombre repuesto"
                    />
                    <q-input
                      v-model="newVehiclePat.stock"
                      label="Cantidad"
                      type="number"
                      mask="int"
                    />
                    <q-input
                      v-model="newVehiclePat.unitPrice"
                      label="Q. (precio unitario)"
                      type="number"
                      mask="int"
                    />
                    <q-separator />
                    <br />
                    <q-btn
                      class="bg-teal text-white"
                      label="Crear repuesto"
                      color="primary"
                      @click="saveVehiclePart"
                    ></q-btn>
                  </q-form>
                </div>
              </q-card-section>
            </q-card>
          </q-card>
        </q-card>
        <!--  -->
      </q-card>
    </q-dialog>
    <!--  -->
  </div>
</template>

<script setup>
import { ref, onMounted, computed } from "vue";
import axios from "axios";
import { useQuasar } from "quasar";

const { localStorage, notify } = useQuasar();
const loading = ref(false);
const rows = ref([]);
const isActiveNewVehicle = ref(true);

const newVehiclePat = ref({
  name: "",
  stock: "",
  unitPrice: "",
});

function cardShowNewVehicleParts() {
  isActiveNewVehicle.value = !isActiveNewVehicle.value;
  newVehiclePat.value.name = "";
  newVehiclePat.value.stock = "";
  newVehiclePat.value.unitPrice = "";
}

function start() {
  localStorage.set("owner", false);
}

async function saveVehiclePart() {
  if (
    !(
      newVehiclePat.value.name == "" ||
      newVehiclePat.value.stock == "" ||
      newVehiclePat.value.unitPrice == ""
    )
  ) {
    try {
      const response = await axios.post(
        process.env.API_BASE_URL + "/taller/VehicleParts",
        {
          name: newVehiclePat.value.name,
          stock: newVehiclePat.value.stock,
          unitPrice: newVehiclePat.value.unitPrice,
        }
      );

      console.log("Repuesto agregado correctamente:", response.data);

      alert("Se agrego el nuevo repuesto");
    } catch (error) {
      console.error("Error al agregar Repuesto:", error);
    }
    isActiveNewVehicle.value = !isActiveNewVehicle.value;
  } else alert("Complete todos los campos");
}

onMounted(start);
</script>
