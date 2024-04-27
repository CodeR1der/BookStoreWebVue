import { reactive } from 'vue';

// �������� ������� �� localStorage, ���� ��� ��� ����
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

        // ��������� ����������� ������� � localStorage
        localStorage.setItem('cartItems', JSON.stringify(state.cartItems));
    },
    removeFromCart(index) {
        state.cartItems.splice(index, 1);
        // ��������� ����������� ������� � localStorage
        localStorage.setItem('cartItems', JSON.stringify(state.cartItems));
    },
    increaseQuantity(item) {
        item.quantity++;
        // ��������� ����������� ������� � localStorage
        localStorage.setItem('cartItems', JSON.stringify(state.cartItems));
    },
    decreaseQuantity(item) {
        if (item.quantity === 1) {
            var index = state.cartItems.lastIndexOf(item);
            this.removeFromCart(index);
        } else {
            item.quantity--;
            // ��������� ����������� ������� � localStorage
            localStorage.setItem('cartItems', JSON.stringify(state.cartItems));
        }
    },
    getTotalCartItems() {
        // ��������� ���������� ���� ���� � �������
        return state.cartItems.reduce((total, item) => total + item.quantity, 0);
    }
};

export default {
    state,
    ...methods
};
