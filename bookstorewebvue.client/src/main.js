import { createApp } from 'vue';
import router from '@/router/index';
import App from './App.vue';
import Auth from './store/auth';
import ShoppingCartStore from '@/ShoppingCartStore.js';

const token = localStorage.getItem('token');
if (token) {
    const user = parseToken(token);
    if (user.role === 'admin') {
        user.isAdmin = true;
    }

    Auth.commit('loginUser', { user, token });
}

const app = createApp(App);
app.use(Auth);
app.use(router);
app.provide('ShoppingCartStore', ShoppingCartStore);
app.mount('#app');
function parseToken(token) {
    const base64Url = token.split('.')[1];
    const base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
    const jsonPayload = decodeURIComponent(atob(base64).split('').map(function (c) {
        return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2);
    }).join(''));

    return JSON.parse(jsonPayload);
}