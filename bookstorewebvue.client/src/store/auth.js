import { createStore } from 'vuex';
import axios from 'axios';

const store = createStore({
    state: {
        currentUser: null,
    },
    mutations: {
        loginUser(state, { user, token }) {
            state.currentUser = user;
            localStorage.setItem('currentUser', JSON.stringify(user));
            localStorage.setItem('token', token); 
        },
        logoutUser(state) {
            state.currentUser = null;
            localStorage.removeItem('currentUser');
            localStorage.removeItem('token'); 
        } 
    },
    actions: {
        async registerUser({ commit }, user) {
            try {
                const response = await axios.post('/authorizations/register', user);
                commit('loginUser', response.data);
            } catch (error) {
                throw new Error(error.response.data.message);
            }
        },
        async loginUser({ commit }, credentials) {
            try {
                const response = await axios.post('/authorizations/login', credentials);
                commit('loginUser', response.data);
            } catch (error) {
                throw new Error(error.response.data.message);
            }
        },
        async logoutUser({ commit }) {
            try {
                await axios.post('/authorizations/logout');
                commit('logoutUser');
            } catch (error) {
                throw new Error(error.response.data.message);
            }
        }
        
    },
    getters: {
        isAdmin: state => {
            return state.currentUser && state.currentUser.isAdmin;
        }
    },
});

export default store;
