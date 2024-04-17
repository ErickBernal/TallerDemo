<template>
  <q-page padding>
    <!-- <div class="row q-col-gutter-sm q-ma-xs">
      <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12"></div>
    </div> -->

    <RudClient />
  </q-page>
</template>

<!-- //////////////////////////////   ////////////////////////////////////// -->
<script setup>
import { ref, onMounted, computed } from "vue";
import { useQuasar, LocalStorage } from "quasar";
import RudClient from "./RudClient.vue";

const start = () => {
  LocalStorage.set("owner", false);
};

onMounted(start);
</script>
