import { createApp } from 'vue';
import router from '@/router/index';
import App from './App.vue';
import Auth from './store/auth';
import ShoppingCartStore from '@/ShoppingCartStore.js';

const app = createApp(App);
app.use(Auth);
app.use(router);
app.provide('ShoppingCartStore', ShoppingCartStore);
app.mount('#app');
