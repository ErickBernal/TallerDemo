import { createApp } from "vue";
import { createRouter, createWebHistory } from "vue-router";
import "./style.css";
import App from "./App.vue";
import Repuesto from "./components/Repuesto.vue";
import Wallcome from "./components/Wallcome.vue";
import NavBar from "./components/NavBar.vue";
import Logs from "./components/Logs.vue";
import Servicios from "./components/Servicios.vue";
import NuevoServicio from "./components/NuevoServicio.vue";
import EstadoServicio from "./components/EstadoServicio.vue";

const routes = [
  { path: "/", component: Wallcome },
  { path: "/repuesto", component: Repuesto },
  { path: "/navbar", component: NavBar },
  { path: "/logs", component: Logs },
  { path: "/servicios", component: Servicios },
  { path: "/nuevoServicio", component: NuevoServicio },
  { path: "/estadoServicio", component: EstadoServicio },
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

createApp(App).use(router).mount("#app");
