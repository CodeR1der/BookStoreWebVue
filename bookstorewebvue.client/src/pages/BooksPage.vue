<template>
    <h1>Каталог книг</h1>
    <div class="row">
        <div class="col-md-3" v-for="book in forsale" :key="book.id">
            <div class="card">
                <img :src="book.coverBase64" class="card-img-top mx-auto d-block">
                <div class="card-body">
                    <h4 class="card-title">{{ book.book.title }}</h4>
                    <h5 class="card-text">{{ book.book.author.authorName.trim().split(' ').slice(0, 2).join(' ') }}</h5>
                    <div class="card-text">{{ book.book.price }} ₽</div>
                    <div class="row justify-content-end">
                        <button class="btn btn-success" @click="addToCart(book.book)">В корзину</button>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<script lang="js">
    import { defineComponent } from 'vue';
    import ShoppingCartStore from '@/ShoppingCartStore.js';


    export default defineComponent({
        data() {
            return {
                loading: false,
                forsale: null
            };
        },
        created() {
            this.fetchData();
        },
        watch: {
            '$route': 'fetchData'
        },
        methods: {
            fetchData() {
                this.forsale = null;
                this.loading = true;

                fetch('books')
                    .then(r => r.json())
                    .then(json => {
                        this.forsale = json;
                        this.loading = false;
                    });
            },
            addToCart(book) {
                ShoppingCartStore.addToCart(book);
            }
        }
    });
</script>
<style>
    .card-img-top {
        width: 128px;
        height: auto;
    }
    .card {
        width: 256px;
        height: auto;
    }
</style>