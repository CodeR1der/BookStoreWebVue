<template>
    <Sidebar />
    <div class="main">
        <h1>Ваши заказы</h1>
        <div v-if="customerOrders.length === 0">У вас пока нет заказов.</div>
        <div v-else>
            <div v-for="order in customerOrders" :key="order.orderId" class="card mb-3">
                <div class="card-body">
                    <h5 class="card-title">Заказ от {{ formatDate(order.orderDate) }}</h5>
                    <p class="card-text">Тип доставки: {{ order.shippingMethod.methodName }}</p>
                    <p class="card-text">Итоговая цена: {{ order.totalPrice }}</p>
                    <p class="card-text">Товары:</p>
                    <ul class="list-group">
                        <li v-for="line in order.orderLines" :key="line.lineID" class="list-group-item">
                            {{ line.book.title }} - ({{ line.quantity }} шт.)
                        </li>
                    </ul>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
    import { computed } from 'vue';
    import { useStore } from 'vuex';
    import axios from 'axios';
    import Sidebar from '@/pages/profile/Profile-sidebar.vue';

    export default {
        name: 'OrdersPage',
        components: {
            Sidebar,
        },
        data() {
            return {
                customerOrders: [],
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
            async fetchOrders() {
                try {
                    const response = await axios.get('/customerOrders');
                    const orders = response.data;
                    this.customerOrders = orders.filter(order => order.customerId === this.currentUser.userId);

                    const orderLinesResponse = await axios.get(`/orderLines`);
                    const orderLines = orderLinesResponse.data;

                    for (const order of this.customerOrders) {
                        order.orderLines = orderLines.filter(line => line.orderId === order.orderId);
                    }
                } catch (error) {
                    console.error('Ошибка при получении заказов:', error);
                }
            },
            formatDate(dateString) {
                const date = new Date(dateString);
                return `${date.getDate()}.${date.getMonth() + 1}.${date.getFullYear()}`;
            }
        },
        created() {
            this.fetchOrders();
        }
    };
</script>

<style scoped>
    .main {
        margin-left: 200px; /* Ширина боковой панели */
        padding: 20px; /* Добавляем некоторый внутренний отступ для основного контента */
    }
</style>
