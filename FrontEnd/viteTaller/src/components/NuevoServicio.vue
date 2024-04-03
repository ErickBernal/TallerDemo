<template>
  <div>
    <div>
      <NavBar />
    </div>
    <div>
      <h2>Nuevo Servicio</h2>
    </div>
  </div>
</template>

<script>
import NavBar from "./NavBar.vue";
export default {
  components: {
    NavBar,
  },
  data() {
    return {
      repuestos: [],
    };
  },
};
</script>

<style scoped>
/* Agrega estilos si es necesario */
</style>
