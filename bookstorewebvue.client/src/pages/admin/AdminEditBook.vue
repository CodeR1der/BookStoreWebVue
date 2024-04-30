<template>
    <div class="modal">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h2 class="modal-title">Редактировать книгу</h2>
                    <button type="button" class="close" @click="closeModal">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <form @submit.prevent="saveChanges">
                    <div class="modal-body">
                        <div class="form-group">
                            <label for="title">Название:</label>
                            <input type="text" class="form-control" id="title" v-model="editedBook.title">
                        </div>
                        <div class="form-group">
                            <label for="author">Автор:</label>
                            <input type="text" class="form-control" id="author" v-model="editedBook.author.authorName">
                        </div>
                        <div class="form-group">
                            <label for="isbn13">ISBN-13:</label>
                            <input type="text" class="form-control" id="isbn13" v-model="editedBook.isbn13">
                        </div>
                        <div class="form-group">
                            <label for="language">Язык:</label>
                            <input type="text" class="form-control" id="language" v-model="editedBook.language.languageName">
                        </div>
                        <div class="form-group">
                            <label for="numPages">Количество страниц:</label>
                            <input type="number" class="form-control" id="numPages" v-model.number="editedBook.numPages">
                        </div>
                        <div class="form-group">
                            <label for="publicationDate">Дата публикации:</label>
                            <input type="number" class="form-control" id="publicationDate" v-model="editedBook.publicationDate">
                        </div>
                        <div class="form-group">
                            <label for="publisher">Издательство:</label>
                            <input type="text" class="form-control" id="publisher" v-model="editedBook.publisher.publisherName">
                        </div>
                        <div class="form-group">
                            <label for="price">Цена:</label>
                            <input type="number" class="form-control" id="price" v-model.number="editedBook.price">
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="submit" class="btn btn-primary">Сохранить</button>
                        <button type="button" class="btn btn-secondary" @click="closeModal">Отмена</button>
                    </div>
                </form>
            </div>
        </div>
    </div>
</template>

<script>
    export default {
        props: {
            book: Object
        },
        data() {
            return {
                editedBook: {}
            };
        },
        watch: {
            book: {
                handler(newBook) {
                    this.editedBook = { ...newBook };
                },
                immediate: true
            }
        },
        methods: {
            saveChanges() {
                this.$emit('save', this.editedBook);
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
    }

    .modal-header {
        border-bottom: 1px solid #dee2e6;
        padding: 15px;
    }

    .modal-title {
        margin-bottom: 0;
    }

    .modal-body {
        padding: 15px;
    }

    .modal-footer {
        border-top: 1px solid #dee2e6;
        padding: 15px;
    }
</style>
