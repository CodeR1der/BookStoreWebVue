import { reactive } from 'vue';

// Получаем корзину из localStorage, если она там есть
const savedCartItems = JSON.parse(localStorage.getItem('cartItems')) || [];

const state = reactive({
    cartItems: savedCartItems
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

        // Сохраняем обновленную корзину в localStorage
        localStorage.setItem('cartItems', JSON.stringify(state.cartItems));
    },
    removeFromCart(index) {
        state.cartItems.splice(index, 1);
        // Сохраняем обновленную корзину в localStorage
        localStorage.setItem('cartItems', JSON.stringify(state.cartItems));
    },
    increaseQuantity(item) {
        item.quantity++;
        // Сохраняем обновленную корзину в localStorage
        localStorage.setItem('cartItems', JSON.stringify(state.cartItems));
    },
    decreaseQuantity(item) {
        if (item.quantity === 1) {
            var index = state.cartItems.lastIndexOf(item);
            this.removeFromCart(index);
        } else {
            item.quantity--;
            // Сохраняем обновленную корзину в localStorage
            localStorage.setItem('cartItems', JSON.stringify(state.cartItems));
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
