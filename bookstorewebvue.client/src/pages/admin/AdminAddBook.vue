<template>
    <div class="modal ">
        <div class="modal-content">
            <h2>Добавить книгу</h2>
            <form @submit.prevent="addBook">
                <div>
                    <label for="title">Название:</label>
                    <input type="text" id="title" v-model="newBook.title">
                </div>
                <div>
                    <label for="author">Автор:</label>
                    <input type="text" id="author" v-model="newBook.authorId">
                </div>
                <div>
                    <label for="isbn13">ISBN-13:</label>
                    <input type="text" id="isbn13" v-model="newBook.isbn13">
                </div>
                <div>
                    <label for="language">Язык:</label>
                    <input type="text" id="language" v-model="newBook.languageId">
                </div>
                <div>
                    <label for="numPages">Количество страниц:</label>
                    <input type="number" id="numPages" v-model="newBook.numPages">
                </div>
                <div>
                    <label for="publicationDate">Дата публикации:</label>
                    <input type="number" id="publicationDate" v-model="newBook.publicationDate">
                </div>
                <div>
                    <label for="publisher">Издательство:</label>
                    <input type="text" id="publisher" v-model="newBook.publisherId">
                </div>
                <div>
                    <label for="genre">Жанр:</label>
                    <input type="text" id="genre" v-model="newBook.genreId">
                </div>
                <div>
                    <label for="price">Цена:</label>
                    <input type="number" id="price" v-model="newBook.price">
                </div>
                <button type="submit">Добавить</button>
                <button @click="closeModal">Отмена</button>
            </form>
        </div>
    </div>
</template>

<script>
    import checkBook from './checkNewBook.js'

    export default {
        data() {
            return {
                newBook: {
                    title: '',
                    authorId: '',
                    author: { authorId: 'aaaaaaaa-bbbb-cccc-dddd-f98a6fa8d347', authorName: '' },
                    isbn13: '',
                    languageId: '',
                    language: { language: 'aaaaaaaa-bbbb-cccc-dddd-f98a6fa8d347', languageCode: '', languageName: '' },
                    numPages: 0,
                    publicationDate: 0,
                    publisherId: '',
                    publisher: { publisherId: 'aaaaaaaa-bbbb-cccc-dddd-f98a6fa8d347', publisherName: '' },
                    genreId: '',
                    genre: { genreId: 'aaaaaaaa-bbbb-cccc-dddd-f98a6fa8d347', genreName: '' },
                    price: 0
                }
            };
        },
        methods: {
            async addBook() {
                try {
                    const book = await checkBook(this.newBook);
                    this.$emit('add', book);
                    this.closeModal();
                } catch (error) {
                    console.error(error);
                }
            },
            closeModal() {
                this.$emit('close');
            }
        }
    };
</script>

<style>
    .modal {
        position: fixed;
        top: 0;
        left: 0;
        width: 100%;
        height: 100%;
        background-color: rgba(0, 0, 0, 0.5);
        display: flex;
        justify-content: center;
        align-items: center;
    }

    .modal-content {
        background-color: #fff;
        padding: 20px;
    }
</style>
