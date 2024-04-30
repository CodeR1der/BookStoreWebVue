<template>
    <div class="modal" tabindex="-1" role="dialog">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h2 class="modal-title">Добавить книгу</h2>
                    <button type="button" class="close" @click="closeModal">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <form @submit.prevent="addBook">
                        <div class="form-group">
                            <label for="title">Название:</label>
                            <input type="text" class="form-control" id="title" v-model="newBook.title">
                        </div>
                        <div class="form-group">
                            <label for="author">Автор:</label>
                            <input type="text" class="form-control" id="author" v-model="newBook.authorId">
                        </div>
                        <div class="form-group">
                            <label for="isbn13">ISBN-13:</label>
                            <input type="text" class="form-control" id="isbn13" v-model="newBook.isbn13">
                        </div>
                        <div class="form-group">
                            <label for="language">Язык:</label>
                            <input type="text" class="form-control" id="language" v-model="newBook.languageId">
                        </div>
                        <div class="form-group">
                            <label for="numPages">Количество страниц:</label>
                            <input type="number" class="form-control" id="numPages" v-model.number="newBook.numPages">
                        </div>
                        <div class="form-group">
                            <label for="publicationDate">Дата публикации:</label>
                            <input type="number" class="form-control" id="publicationDate" v-model="newBook.publicationDate">
                        </div>
                        <div class="form-group">
                            <label for="publisher">Издательство:</label>
                            <input type="text" class="form-control" id="publisher" v-model="newBook.publisherId">
                        </div>
                        <div class="form-group">
                            <label for="genre">Жанр:</label>
                            <input type="text" class="form-control" id="genre" v-model="newBook.genreId">
                        </div>
                        <div class="form-group">
                            <label for="price">Цена:</label>
                            <input type="number" class="form-control" id="price" v-model.number="newBook.price">
                        </div>
                    </form>
                    <div class="modal-footer">
                        <button type="submit" class="btn btn-primary">Сохранить</button>
                        <button type="button" class="btn btn-secondary" @click="closeModal">Отмена</button>
                    </div>
                </div>
            </div>
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
        background-color: rgba(0, 0, 0, 0.5);
    }

    .modal-dialog {
        margin-top: 100px;
    }

    .modal-content {
        background-color: #fff;
        padding: 20px;
    }
    .modal-footer {
        border-top: 1px solid #dee2e6;
        padding: 15px;
    }
</style>
