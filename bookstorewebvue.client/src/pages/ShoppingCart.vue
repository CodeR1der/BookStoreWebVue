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
                        
                        <h5 class="mt-3">Итого: {{ totalCartPrice }} ₽ </h5>
                        <button>Оформить заказ</button>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
    import ShoppingCartStore from '@/ShoppingCartStore.js';

    export default {
        computed: {
            cartItems() {
                return ShoppingCartStore.state.cartItems;
            },
            totalCartPrice() {
                 return this.cartItems.reduce((total, item) => total + item.price * item.quantity, 0);
            }
        },
        methods: {
            removeFromCart(index) {
                ShoppingCartStore.removeFromCart(index);
            },
            increase(item) {
                return ShoppingCartStore.increaseQuantity(item);
            },
            decrease(item) {
                return ShoppingCartStore.decreaseQuantity(item);
            }
        }
    };
</script>
