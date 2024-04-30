<template>
    <div>
        <Sidebar />
        <div class="main">
            <h2>Адрес доставки</h2>
            <form>
                <div class="mb-3">
                    <label for="country" class="form-label">Страна:</label>
                    <select id="country" v-model="address.countryId" class="form-control">
                        <option value="">Выберите страну</option>
                        <option v-for="country in countries" :value="country.countryId" :key="country.countryId">{{ country.countryName }}</option>
                    </select>
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
                <button @click.prevent="updateAddress" class="btn btn-primary">Сохранить</button>
            </form>
        </div>
    </div>
</template>

<script>
    import { computed } from 'vue';
    import { useStore } from 'vuex';
    import Sidebar from '@/pages/profile/Profile-sidebar.vue';
    import axios from 'axios';

    export default {
        components: {
            Sidebar
        },
        data() {
            return {
                countries: [], // Список стран
                address: {
                    city: '',
                    streetName: '',
                    streetNumber: '',
                    countryId: ''
                }
            };
        },
        setup() {
            const store = useStore();
            const currentUser = computed(() => store.state.currentUser);

            return {
                currentUser
            };
        },
        methods: {
            async fetchCountries() {
                try {
                    const response = await axios.get('/countrys');
                    this.countries = response.data;
                } catch (error) {
                    console.error('Ошибка при загрузке списка стран:', error);
                }
            },
            async fetchAddress() {
                try {
                    const response = await axios.get(`/addresses/${this.currentUser.addressId}`);
                    const addressData = response.data;
                    this.address = {
                        city: addressData.city,
                        streetName: addressData.streetName,
                        streetNumber: addressData.streetNumber,
                        countryId: addressData.countryId
                    };
                } catch (error) {
                    console.error('Ошибка при загрузке адреса:', error);
                }
            },
            async updateAddress() {
                // Ваш метод updateAddress остается без изменений
            }
        },
        created() {
            this.fetchCountries(); // Выполняем загрузку списка стран при создании компонента
            if (this.currentUser.addressId !== '') {
                this.fetchAddress(); // Получаем информацию об адресе, если addressId не пустой
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