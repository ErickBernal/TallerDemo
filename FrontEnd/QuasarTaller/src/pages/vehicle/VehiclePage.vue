<template>
  <div class="row q-col-gutter-sm q-ma-xs">
    <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
      <NewVehicle />
    </div>
    <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
      <RudVehicle />
    </div>
  </div>
</template>

<script setup>
import NewVehicle from "./NewVehicle.vue";
import RudVehicle from "./RudVehicle.vue";
</script>
