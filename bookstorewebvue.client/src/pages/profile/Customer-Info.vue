<template>
    <div>
        <Sidebar />
        <div class="main">
            <h2>Личная информация</h2>
            <form>
                <div class="mb-3">
                    <label for="firstname" class="form-label">Имя:</label>
                    <input type="text" id="firstname" v-model="editedCustomer.firstName" class="form-control">
                </div>
                <div class="mb-3">
                    <label for="lastname" class="form-label">Фамилия:</label>
                    <input type="text" id="lastname" v-model="editedCustomer.lastName" class="form-control">
                </div>
                <div class="mb-3">
                    <label for="email" class="form-label">Email:</label>
                    <input type="text" id="email" v-model="editedCustomer.email" class="form-control" disabled>
                </div>
                <button @click.prevent="updateCustomer" class="btn btn-primary">Сохранить</button>
            </form>
        </div>
    </div>
</template>

<script>
    import axios from 'axios';
    import { computed } from 'vue';
    import { useStore } from 'vuex';
    import Sidebar from '@/pages/profile/Profile-sidebar.vue';

    export default {
        components: {
            Sidebar
        },
        setup() {
            const store = useStore();
            const currentUser = computed(() => store.state.currentUser);

            return {
                currentUser
            };
        },
        data() {
            return {
                editedCustomer: {}
            };
        },
        methods: {
            async getCustomers() {
                try {
                    const response = await axios.get(`customers`);
                    return response.data;
                } catch (error) {
                    console.error(error);
                    return [];
                }
            },
            async updateCustomer() {
                try {
                    await axios.put(`customers/${this.editedCustomer.customerId}/update`, this.editedCustomer);
                } catch (error) {
                    console.error(error);
                }
            },
            async fetchCustomerData() {
                try {
                    const email = this.currentUser.email;
                    const response = await axios.get(`customers`);
                    const customers = response.data;
                    const customer = customers.find(c => c.email === email);
                    if (customer) {
                        this.editedCustomer = customer;
                    }
                } catch (error) {
                    console.error(error);
                }
            }
        },
        mounted() {
            this.fetchCustomerData();
        }
    };
</script>

<style scoped>
    .main {
        margin-left: 200px; /* Ширина боковой панели */
        padding: 20px; /* Добавляем некоторый внутренний отступ для основного контента */
    }
</style>
