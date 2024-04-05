<template>
  <q-page paddiing>
    <h1>New Services</h1>
  </q-page>
</template>

<script>
import { ref, onMounted, computed } from "vue";
import { useQuasar } from "quasar";

const $q = useQuasar();

function start() {
  $q.localStorage.set("owner", false);
}
onMounted(start);
</script>
