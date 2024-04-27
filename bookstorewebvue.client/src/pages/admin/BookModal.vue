<template>
    <div class="modal">
        <div class="modal-content">
            <h2>Редактирование книги</h2>
            <form @submit.prevent="saveBook">
                <label>Название:</label>
                <input type="text" v-model="editedBook.title">
                <label>Автор:</label>
                <input type="text" v-model="editedBook.author">
                <label>Цена:</label>
                <input type="number" v-model="editedBook.price">
                <button type="submit">Сохранить</button>
            </form>
            <button @click="closeModal">Закрыть</button>
        </div>
    </div>
</template>

<script>
export default {
  props: ['book'],
  data() {
    return {
      editedBook: { ...this.book }
    }
  },
  methods: {
    saveBook() {
      // Отправить данные отредактированной книги на сервер
      this.$emit('save', this.editedBook);
      // Закрыть модальное окно
      this.closeModal();
    },
    closeModal() {
      this.$emit('close');
    }
  }
}
</script>