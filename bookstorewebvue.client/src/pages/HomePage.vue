<template>
    <div>
        <h1>Главная страница</h1>
        <div v-for="(genreBooks, genre) in categorizedBooks" :key="genre">
            <h2>{{ genre }}</h2>
            <div class="row">
                <div class="col-md-3" v-for="book in genreBooks" :key="book.id">
                    <div class="card">
                        <img :src="book.coverBase64" class="card-img-top mx-auto d-block" width="640" />
                        <div class="card-body">
                            <h4 class="card-title">{{ book.title }}</h4>
                            <h5 class="card-text">{{ book.author }}</h5>
                            <div class="card-text">{{ book.price }} ₽</div>
                            <div class="row justify-content-end">
                                <button class="btn btn-success" @click="addToCart(book)">В корзину</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
    import axios from 'axios';

    export default {
        data() {
            return {
                categorizedBooks: {}
            };
        },
        mounted() {
            this.getBooks();
        },
        methods: {
            async getBooks() {
                try {
                    const response = await axios.get('books');
                    const books = response.data;
                    this.categorizedBooks = this.categorizeBooks(books);
                } catch (error) {
                    console.error(error);
                }
            },
            categorizeBooks(books) {
                const categorizedBooks = {};
                books.forEach(book => {
                    const genre = book.book.genre.genreName;
                    if (!categorizedBooks[genre]) {
                        categorizedBooks[genre] = [];
                    }
                    categorizedBooks[genre].push({
                        id: book.book.bookId,
                        title: book.book.title,
                        author: book.book.author.authorName.trim().split(' ').slice(0, 2).join(' '),
                        coverBase64: book.coverBase64,
                        price: book.book.price
                    });
                });
                return categorizedBooks;
            },
            addToCart(book) {
                // Действия по добавлению книги в корзину
            }
        }
    };
</script>

<style>
    /* Стили для карточек книг и изображений */
</style>
