<script>
import MyComponent from "./components/MyComponent.vue";
import Wallcome from "./components/Wallcome.vue";
import NavBar from "./components/NavBar.vue";
export default {
  name: "App",
  components: {
    Wallcome,
  },
};
</script>

<template>
  <RouterView />
  <div>
    <NavBar />
  </div>
</template>

<style></style>
