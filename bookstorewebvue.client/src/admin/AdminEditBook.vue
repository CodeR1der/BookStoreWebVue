<template>
    <div class="modal">
        <div class="modal-content">
            <h2>Редактировать книгу</h2>
            <form @submit.prevent="saveChanges">
                <div>
                    <label for="title">Название:</label>
                    <input type="text" id="title" v-model="editedBook.title">
                </div>
                <div>
                    <label for="author">Автор:</label>
                    <input type="text" id="author" v-model="editedBook.author.authorName">
                </div>
                <div>
                    <label for="isbn13">ISBN-13:</label>
                    <input type="text" id="isbn13" v-model="editedBook.isbn13">
                </div>
                <div>
                    <label for="language">Язык:</label>
                    <input type="text" id="language" v-model="editedBook.language.languageName">
                </div>
                <div>
                    <label for="numPages">Количество страниц:</label>
                    <input type="number" id="numPages" v-model.number="editedBook.numPages">
                </div>
                <div>
                    <label for="publicationDate">Дата публикации:</label>
                    <input type="number" id="publicationDate" v-model="editedBook.publicationDate">
                </div>
                <div>
                    <label for="publisher">Издательство:</label>
                    <input type="text" id="publisher" v-model="editedBook.publisher.publisherName">
                </div>
                <div>
                 </div>
                <div>
                    <label for="price">Цена:</label>
                    <input type="number" id="price" v-model.number="editedBook.price">
                </div>
                <button type="submit">Сохранить</button>
                <button @click="closeModal">Отмена</button>
            </form>
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
        padding: 20px;
    }
</style>

