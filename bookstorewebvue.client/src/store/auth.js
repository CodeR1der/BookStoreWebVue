import { createStore } from 'vuex';
import { useRouter } from 'vue-router';

const initialUsers = [
    {
        email: 'admin@admin.com',
        password: 'qwerty123456', 
        isAdmin: true,
        nickname: 'Admin',
    },
    {
        email: 'user@user.com',
        password: 'qwerty12345',
        isAdmin: false,
        nickname: 'user',
    }
];


const store = createStore({
    state: {
        users: initialUsers,
        currentUser: null,
    },
    mutations: {
        registerUser(state, user) {
            state.users.push(user);
            localStorage.setItem('users', JSON.stringify(state.users));
        },
        loginUser(state, user) {
            state.currentUser = user;
            localStorage.setItem('currentUser', JSON.stringify(user));
        },
        logoutUser(state) {
            state.currentUser = null;
            localStorage.removeItem('currentUser');
            // Очищаем список пользователей при выходе из аккаунта
            state.users = [];
            localStorage.removeItem('users');
        },
    },
    actions: {
        registerUser({ commit, state }, user) {
            return new Promise((resolve, reject) => {
                const existingUser = state.users.find(u => u.email === user.email);
                if (existingUser) {
                    reject(new Error('User with this email already exists'));
                } else {
                    if (!isStrongPassword(user.password)) {
                        reject(new Error('Password must contain at least 6 characters, including at least one letter and one digit'));
                        return;
                    }

                    commit('registerUser', user);
                    commit('loginUser', user);
                    resolve();
                }
            });
        },
        loginUser({ commit, state }, credentials) {
            const user = state.users.find(u => u.email === credentials.email && u.password === credentials.password);
            if (user) {
                commit('loginUser', user);
                const router = useRouter();
                if (user.isAdmin) {
                    router.push('/admin');
                }
                else {
                    router.push('/books');
                }
            } else {
                throw new Error('Invalid credentials');
            }
        },
        logoutUser({ commit }) {
            commit('logoutUser');
        },
    },
    getters: {
        // Ваши getters
        isAdmin: state => {
            return state.currentUser && state.currentUser.isAdmin;
        }
    }
});

// Проверка качества пароля
function isStrongPassword(password) {
    const passwordRegex = /^(?=.*[a-zA-Z])(?=.*\d).{6,}$/;
    return passwordRegex.test(password);
}

// Проверяем localStorage при загрузке приложения
const currentUserJSON = localStorage.getItem('currentUser');
if (currentUserJSON) {
    const currentUser = JSON.parse(currentUserJSON);
    store.commit('loginUser', currentUser);
}

export default store;
