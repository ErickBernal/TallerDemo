<template>
  <div class="row q-col-gutter-sm q-ma-xs">
    <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
      <FindVehicleParts />
    </div>
    <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
      <NewVehiclePart />
    </div>
  </div>
</template>
<!-- ------------------------------------------------------------------------------ -->
<script setup>
import FindVehicleParts from "./RUDVehicleParts.vue";
import NewVehiclePart from "./NewVehiclePart.vue";
</script>
