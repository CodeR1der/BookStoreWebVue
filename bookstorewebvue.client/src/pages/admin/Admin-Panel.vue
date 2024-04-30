<template>
    <div>
        <h1>Список книг</h1>
        <div class="table-responsive">
            <table class="table table-striped">
                <thead>
                    <tr>
                        <th>Обложка</th>
                        <th>Название</th>
                        <th>Цена</th>
                        <th>Автор</th>
                        <th>Издатель</th>
                        <th>Действия</th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="book in books" :key="book.bookId">
                        <td>
                            <img :src="'../imageBooks/${book.bookId}/${book.title}-1'" class="img-fluid" style="max-height: 100px;" />
                        </td>
                        <td>{{ book.title }}</td>
                        <td>{{ book.price }} ₽</td>
                        <td>{{ book.author.authorName.trim().split(' ').slice(0, 2).join(' ') }}</td>
                        <td>{{ book.publisher.publisherName }}</td>
                        <td>
                            <button @click="openEditModal(book)" class="btn btn-primary"><i class="fa fa-pencil" aria-hidden="true"></i></button>
                            <button @click="deleteBook(book.bookId)" class="btn btn-danger"><i class="fa fa-trash"></i></button>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>

        <AdminEditBook v-if="showEditModal" :book="selectedBook" @close="closeEditModal" @save="updateBook" />
        <button @click="openAddModal" class="btn btn-success">Добавить книгу</button>
        <AdminAddBook v-if="showAddModal" @close="closeAddModal" @add="addBook" />

    </div>
</template>

<script>
    import axios from 'axios';
    import AdminEditBook from './AdminEditBook.vue';
    import AdminAddBook from './AdminAddBook.vue';

    export default {
        components: {
            AdminEditBook,
            AdminAddBook
        },
        data() {
            return {
                books: [],
                showEditModal: false,
                showAddModal: false,
                selectedBook: null
            };
        },
        mounted() {
            this.getBooks();
        },
        methods: {
            async getBooks() {
                try {
                    const response = await axios.get('books');
                    this.books = response.data;
                } catch (error) {
                    console.error(error);
                }
            },
            async deleteBook(bookId) {
                try {
                    await axios.delete(`books/${bookId}/delete`);
                    this.books = this.books.filter(book => book.bookId !== bookId);
                } catch (error) {
                    console.error(error);
                }
            },
            openEditModal(book) {
                this.selectedBook = { ...book };
                this.showEditModal = true;
            },
            closeEditModal() {
                this.showEditModal = false;
            },
            async updateBook(updatedBook) {
                try {
                    await axios.put(`books/${updatedBook.bookId}/update`, updatedBook);
                    const index = this.books.findIndex(book => book.bookId === updatedBook.bookId);
                    if (index !== -1) {
                        this.books.splice(index, 1, updatedBook);
                    }
                    this.closeEditModal();
                } catch (error) {
                    console.error(error);
                }
            },
            openAddModal() {
                this.showAddModal = true;
            },
            closeAddModal() {
                this.showAddModal = false;
            },
            async addBook(newBook) {
                try {
                    const bookData = {...newBook};
                    const response = await axios.post('books/post', bookData);
                    this.books.push(response.data);
                    this.closeAddModal();
                } catch (error) {
                    console.error(error);
                }
            }
        }
    };
</script>
