const state = {
    items: [], // массив объектов для хранения книг в корзине
};

const getters = {
    cartItems: state => state.items,
    totalPrice: state => {
        return state.items.reduce((total, item) => total + item.price, 0);
    },
};

const mutations = {
    addToCart(state, book) {
        state.items.push(book);
    },
    removeFromCart(state, index) {
        state.items.splice(index, 1);
    },
    clearCart(state) {
        state.items = [];
    },
};

const actions = {
    addToCart({ commit }, book) {
        commit('addToCart', book);
    },
    removeFromCart({ commit }, index) {
        commit('removeFromCart', index);
    },
    clearCart({ commit }) {
        commit('clearCart');
    },

};

export default {
    state,
    getters,
    mutations,
    actions,
};