<template>
  <!-- <q-page> -->
  <!-- <div class="row q-col-gutter-sm q-ma-xs"> -->
  <!-- <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12"> -->
  <q-card class="my-card" flat bordered shadow-xl>
    <q-card-section class="bg-teal text-white">
      <div class="text-h5 q-mt-sm q-mb-xs">
        <q-avatar
          color="white"
          text-color="primary"
          size="30px"
          class="q-mr-md"
        >
          <q-icon
            v-if="dataToShow.statusServices === 1"
            name="check_circle"
            color="green"
            size="25px"
          ></q-icon>

          <q-icon
            v-if="dataToShow.statusServices === 2"
            name="check_circle"
            color="yellow"
            size="25px"
          ></q-icon>
          <q-icon
            v-else-if="dataToShow.statusServices === 3"
            name="cancel"
            color="red"
            size="25px"
          ></q-icon>
        </q-avatar>
        Estado de servicio
      </div>
      <!--  -->
    </q-card-section>
    <q-card-section horizontal>
      <!--  -->
      <q-card-section class="q-pt-xs col-7">
        <div class="text-overline" v-if="dataToShow.statusServices === 3">
          En espera de atencion
        </div>
        <div class="text-overline" v-if="dataToShow.statusServices === 2">
          En proceso, Realizando mantenimiento
        </div>
        <div class="text-overline" v-if="dataToShow.statusServices === 1">
          Finalizado, Listo para entrega
        </div>

        <div class="text-h5 q-mt-sm q-mb-xs">
          {{ dataToShow.name }} {{ dataToShow.lastName }}
        </div>
        <q-separator />
        <div class="text-caption text-grey">
          <div>
            <i> Tipo de servicio:</i> <b>{{ dataToShow.serviceType }}</b>
          </div>
          <div>
            <i> Dpi:</i> <b>{{ dataToShow.dpi }}</b>
          </div>
          <div>
            <i> Nit:</i> <b>{{ dataToShow.nit }}</b>
          </div>
          <div>
            <i> Telefono:</i> <b>{{ dataToShow.phone }}</b>
          </div>
          <!-- <FindVehicle
            :sendDataParentToChild="sendDataParentToChild"
            @enviarDatosPadre="getDataFromChild"
          /> -->

          <div>
            <i> Celular:</i> <b>{{ dataToShow.cellphone }}</b>
          </div>
        </div>
        <!--  -->
        <FindVehicle
          :sendDataParentToChild="sendDataParentToChild"
          @enviarDatosPadre="getDataFromChild"
        />

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
      <b> Descripcion del servicio:</b>
      <br />
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
<script setup>
import { ref, onMounted } from "vue";
import { LocalStorage } from "quasar";
import FindVehicle from "../vehicleParts/FindVehicle.vue";
import axios from "axios";
//------------------------------------
//prop envio de datos padre a hijo
const sendDataParentToChild = ref([]);
//chat data child to parent
const getDataFromChild = (datos) => {
  sendDataParentToChild.value = datos;
  // console.log("uno: " + JSON.stringify(datos));
  getVehicleSelected(datos);
};

//------------------------------------
function getVehicleSelected(vSelected) {
  vehicleSelected.value = vSelected;
  // console.log("vehicleSelected: " + JSON.stringify(vehicleSelected.value));
  // console.log(
  //   "vehicleSelected.value.id: " + JSON.stringify(vehicleSelected.value.id)
  // );
  getServiceDetalleByVehicleId(vehicleSelected.value.id);
  // loadDataToShow(servicesDetalle);
  findVehicleCalled.value = true; // Marcar que FindVehicle se ha llamado
}
//------------------------------------

// import FindVehicle from "@/vehicleParts/FindVehicle.vue";
const start = () => {
  LocalStorage.set("owner", false);
};
const vehicleSelected = ref([]);
const servicesDetalle = ref([]);
const accessLoadData = ref(true);
const findVehicleCalled = ref(false);
const dataToShow = ref({
  km: 0,
  serviceType: "",
  statusServices: 0,
  name: "",
  lastName: "",
  dpi: 0,
  nit: 0,
  phone: 0,
  cellphone: 0,
});

async function getServiceDetalleByVehicleId(vehicleId) {
  try {
    const response = await axios.get(
      process.env.API_BASE_URL + "/taller/ServiceDetalle/" + vehicleId
    );
    servicesDetalle.value = response.data;
    loadDataToShow(response.data);
  } catch (error) {
    console.error("Error al obtener departamentos:", error);
    alert("Error al obtener ServiceDetalle");
  }
}
function loadDataToShow(val) {
  // console.log("-----km-------> " + JSON.stringify(val));
  dataToShow.value.km = val.Km;
  dataToShow.value.serviceType = val.ServiceType.Type;
  dataToShow.value.statusServices = val.State;
  dataToShow.value.name = val.Client.Name;
  dataToShow.value.lastName = val.Client.LastName;
  dataToShow.value.dpi = val.Client.DPI;
  dataToShow.value.nit = val.Client.Nit;
  dataToShow.value.phone = val.Client.Phone;
  dataToShow.value.cellphone = val.Client.Cellphone;
  // console.log("············································");
  // console.log("-----km-------> " + JSON.stringify(val.value));
}

onMounted(start);
</script>
