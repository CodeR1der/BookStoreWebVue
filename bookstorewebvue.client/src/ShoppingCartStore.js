import { reactive } from 'vue';

const state = reactive({
    cartItems: []
});

const methods = {
    addToCart(book) {
        const existingBookIndex = state.cartItems.findIndex(item => item.bookId === book.bookId);

        if (existingBookIndex !== -1) {
            state.cartItems[existingBookIndex].quantity++;
        } else {
            book.quantity = 1;
            state.cartItems.push(book);
        }
    },
    removeFromCart(index) {
        state.cartItems.splice(index, 1);
    },
    increaseQuantity(item) {
        item.quantity++;
    },
    decreaseQuantity(item) {
        if (item.quantity === 1) {
            var index = state.cartItems.lastIndexOf(item);
            this.removeFromCart(index);
        }
        else {
            item.quantity--;
        }
    },
    getTotalCartItems() {
        // Суммируем количество всех книг в корзине
        return state.cartItems.reduce((total, item) => total + item.quantity, 0);
    }
};

export default {
    state,
    ...methods
};