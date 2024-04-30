<template>
    <div class="container-fluid">
        <div class="row">
            <div class="col-md-8">
                <h1>Корзина</h1>
                <div v-if="cartItems.length === 0">
                    <p>Корзина пуста</p>
                </div>
                <div v-else>
                    <div v-for="(item, index) in cartItems" :key="index" class="card mb-3 mx-3">
                        <div class="card-body">
                            <h5 class="card-title">{{ item.title }}</h5>
                            <h6 class="card-subtitle mb-2 text-muted">{{ item.author.authorName }}</h6>
                            <p class="card-text">Цена: {{ item.price }} | Количество: <button @click="decrease(item)" class="btn btn-primary btn-sm">-</button> {{ item.quantity }} <button @click="increase(item)" class="btn btn-primary btn-sm">+</button></p>
                            <button @click="removeFromCart(index)" class="btn btn-danger">Удалить</button>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-4">
                <div class="card">
                    <div class="card-body">
                        <h5 class="card-title">Товары в корзине</h5>
                        <ul class="list-group">
                            <li class="list-group-item" v-for="(item, index) in cartItems" :key="index">
                                {{ item.title }} - {{ item.quantity }} шт. - {{ item.price * item.quantity }} ₽
                            </li>
                        </ul>
                        <select v-model="selectedShippingMethod" class="form-select">
                            <option disabled value="">Выберите способ доставки</option>
                            <option v-for="method in shippingMethods" :key="method.methodId" :value="method.methodId">{{ method.methodName }} - {{ method.cost }} ₽</option>
                        </select>
                        <h5 class="mt-3">Итого: {{ totalCartPrice }} ₽ </h5>
                        <button @click="createOrder">Оформить заказ</button>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
    import ShoppingCartStore from '@/ShoppingCartStore.js';
    import axios from 'axios';
    import { computed } from 'vue';
    import { useStore } from 'vuex';

    export default {
        setup() {
            const store = useStore();
            const currentUser = computed(() => store.state.currentUser);

            return {
                currentUser
            };
        },
        data() {
            return {
                shippingMethods: [],
                selectedShippingMethod: null,
                totalCartPrice: 0,
                order: {}
            };
        },
        computed: {
            cartItems() {
                return ShoppingCartStore.state.cartItems;
            }
        },
        watch: {
            selectedShippingMethod() {
                this.updateTotalPrice();
            },
            cartItems: {
                handler: 'updateTotalPrice',
                deep: true
            }
        },
        methods: {
            async createOrder() {
                if (this.cartItems.length !== 0 && this.selectedShippingMethod) {
                    const orderResponse = await axios.post(`/customerorders/post`, {
                        orderDate: new Date(),
                        customerId: this.currentUser.userId,
                        shippingMethodId: this.selectedShippingMethod,
                        destAddressId: this.currentUser.addressId,
                        totalPrice: this.totalCartPrice
                    });
                    const orderId = orderResponse.data.orderId;
                    await Promise.all(this.cartItems.map(async (item) => {
                        await axios.post('/orderLines/post', {
                            orderId: orderId,
                            bookId: item.bookId, 
                            quantity: item.quantity
                        });
                    }));
                    ShoppingCartStore.clearCart();
                }
            },
            removeFromCart(index) {
                ShoppingCartStore.removeFromCart(index);
            },
            increase(item) {
                ShoppingCartStore.increaseQuantity(item);
            },
            decrease(item) {
                ShoppingCartStore.decreaseQuantity(item);
            },
            async fetchMethods() {
                try {
                    const response = await axios.get('/shippingMethods');
                    this.shippingMethods = response.data;
                    // Установка первого способа доставки по умолчанию
                    this.selectedShippingMethod = this.shippingMethods[0].methodId;
                } catch (error) {
                    console.error('Ошибка при загрузке списка способов доставки:', error);
                }
            },
            updateTotalPrice() {
                const selectedMethod = this.shippingMethods.find(method => method.methodId === this.selectedShippingMethod);
                if (selectedMethod) {
                    this.totalCartPrice = this.cartItems.reduce((total, item) => total + item.price * item.quantity, 0) + selectedMethod.cost;
                }
            }
        },

        created() {
            this.fetchMethods()
        }
    };
</script>

