<template>
    <div>
        <nav class="sidebar">
            <div class="sidebar-sticky">
                <ul class="nav flex-column">
                    <li class="nav-item">
                        <router-link to="/info" class="nav-link">Личная информация</router-link>
                    </li>
                    <li class="nav-item">
                        <router-link to="/address" class="nav-link">Адрес</router-link>
                    </li>
                    <li class="nav-item">
                        <router-link to="/orders" class="nav-link">Заказы</router-link>
                    </li>
                </ul>
            </div>
        </nav>
    </div>
</template>

<script>
    export default {
        name: "Sidebar"
    }
</script>

<style>
    .sidebar {
        position: fixed;
        top: 0;
        left: 0;
        bottom: 0;
        padding-top: 4rem; /* Высота header */
        width: 200px; /* Ширина боковой панели */
        background-color: #fff; /* Белый фон */
        color: #000; /* Черный цвет текста */
        box-shadow: -5px 0 5px -5px rgba(0, 0, 0, 0.5); /* Черная полоса по правому краю */
    }

    .sidebar-sticky {
        position: relative;
        top: 4rem; /* Высота header */
        padding-top: 1rem; /* Добавляем небольшой отступ сверху */
    }
</style>