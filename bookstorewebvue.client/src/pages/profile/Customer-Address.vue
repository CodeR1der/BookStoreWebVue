<template>
    <Sidebar />
    <div class="main">
        <h2>Адрес доставки</h2>
        <form>
            <div class="mb-3">
                <label for="country" class="form-label">Страна:</label>
                <input type="text" id="country" v-model="address.country.countryName" class="form-control">
            </div>
            <div class="mb-3">
                <label for="city" class="form-label">Город:</label>
                <input type="tel" id="city" v-model="address.city" class="form-control">
            </div>
            <div class="mb-3">
                <label for="streetName" class="form-label">Название улицы:</label>
                <input type="text" id="streetName" v-model="address.streetName" class="form-control">
            </div>
            <div class="mb-3">
                <label for="streetNumber" class="form-label">Номер дома:</label>
                <input type="text" id="streetNumber" v-model="address.streetNumber" class="form-control">
            </div>
            <button @click.prevent="updateCustomer" class="btn btn-primary">Сохранить</button>
        </form>
    </div>
</template>

<script>
    import { computed, ref, onBeforeMount } from 'vue';
    import { useStore } from 'vuex';
    import Sidebar from '@/pages/profile/Profile-sidebar.vue';
    import axios from 'axios';

    export default {
        components: {
            Sidebar
        },
        setup() {
            const store = useStore();
            const currentUser = computed(() => store.state.currentUser);
            const address = ref({}); // Используем реактивную ссылку

            onBeforeMount(async () => {
                try {
                    const response = await axios.get(`addresses/${currentUser.value.addressId}`);
                    address.value = response.data;
                } catch (error) {
                    console.error(error);
                }
            });

            return {
                currentUser,
                address
            };
        },
        methods: {
            async updateAddress() {
                // Метод обновления адреса
            }
        }
    }

</script>



<style>
    .main {
        margin-left: 200px; /* Ширина боковой панели */
        padding: 20px; /* Добавляем некоторый внутренний отступ для основного контента */
    }
</style>