import { createRouter, createWebHistory } from 'vue-router'
import Home from '../pages/HomePage.vue'
import Books from '../pages/BooksPage.vue'
import ShoppingCart from '../pages/ShoppingCart.vue'
import Admin from '../pages/admin/Admin-Panel.vue'
import Login from "../pages/Login.vue";
import Register from "../pages/Register.vue";
import auth from '@/store/auth';
import ProfileInfo from '../pages/profile/Customer-Info.vue'
import CustomerAddress from '../pages/profile/Customer-Address.vue'
import CustomerOrders from '../pages/profile/Customer-Orders.vue'


const routes = [
    { path: '/', component: Home },
    { path: '/books', component: Books },
    { path: '/cart', component: ShoppingCart },
    { path: '/login', component: Login },
    { path: '/register', component: Register },
    {
        path: '/admin', component: Admin,
        meta: { requiresAdmin: true }
    },
    { path: '/info', component: ProfileInfo },
    { path: '/address', component: CustomerAddress },
    { path: '/orders', component: CustomerOrders }
]

const router = createRouter({
    history: createWebHistory(),
    routes
})
router.beforeEach((to, from, next) => {
    // ѕровер€ем мета-поле requiresAdmin и текущего пользовател€ при каждой навигации
    if (to.meta.requiresAdmin && (!auth.state.currentUser || !auth.state.currentUser.isAdmin)) {
        next({ path: '/login' }); // ≈сли текущий пользователь не €вл€етс€ администратором, перенаправл€ем его на главную страницу
    } else {
        next(); // ѕродолжаем навигацию
    }
});

export default router