<template>
    <div class="header navbar navbar-expand-lg navbar-light bg-light">
        <div class="container">
            <router-link to="/" class="navbar-brand">
                <img src="/src/assets/logo.png" height="50" alt="Логотип">
            </router-link>
            <div class="navbar-nav flex-grow-1">
                <div class="input-group">
                    <input type="text" class="form-control" placeholder="Поиск...">
                    <button type="submit" class="btn btn-primary">Найти</button>
                </div>
            </div>
            <div v-if="isAdmin" class="navbar-nav">
                <router-link to="/admin" class="nav-link">Админ панель</router-link>
            </div>
            <router-link to="/Books" class="nav-link">Каталог</router-link>
            <router-link to="/Cart" class="nav-link"><i class="fa fa-shopping-cart" aria-hidden="true"></i><sub>{{ cartItemCount }}</sub></router-link>
            <div v-if="currentUser" class="navbar-nav">
                <router-link to="/info" class="nav-link">{{currentUser.nickname}}</router-link>
                <button @click="logout" class="btn btn-outline-danger">Выход</button>
            </div>
            <div v-else class="navbar-nav">
                <router-link to="/login" class="nav-link">Вход</router-link>
                <router-link to="/register" class="nav-link">Регистрация</router-link>
            </div>
        </div>
    </div>
</template>

<script>
    import ShoppingCartStore from '@/ShoppingCartStore.js';
    import { computed } from 'vue';
    import { useStore } from 'vuex';
    import { useRouter } from 'vue-router';

    export default {
        setup() {
            const store = useStore();
            const router = useRouter();

            const currentUser = computed(() => store.state.currentUser);
            const isAdmin = computed(() => currentUser.value && currentUser.value.isAdmin);

            const logout = () => {
                store.dispatch('logoutUser');
                router.push('/');
            };

            return { currentUser, isAdmin, logout };
        },
        computed: {
            cartItemCount() {
                return ShoppingCartStore.getTotalCartItems();
            }
        }
    };
</script>

<style scoped>
    .header {
        background-color: #f8f9fa; /* Используем Bootstrap цвет фона */
        z-index: 999;
    }

        .header .container {
            display: flex;
            justify-content: space-between;
            gap: 20px; /* Добавляем расстояние между элементами */
        }

    .input-group {
        display: flex;
        align-items: center;
    }

        .input-group input {
            margin-right: 10px;
        }

    .logo img {
        vertical-align: middle;
    }
</style>
