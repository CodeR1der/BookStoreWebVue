<template>
    <div class="col-md-3">
        <div class="card">
            <img :src="book.image" :alt="book.name" class="card-img-top">
            <div class="card-body">
                <h4 class="card-title">{{ book.title }}</h4>
                <h5 class="card-text">{{ book.author.authorName.trim().split(' ').slice(0, 2).join(' ') }}</h5>
                <div class="card-text">{{ book.price }} ₽</div>
                <div class="row justify-content-end">
                    <button class="btn btn-success" :disabled="book.quantity === 0" @click="addToCart(book)">В корзину</button>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
export default {
    name: 'Item',
    props: ['item'],
}
</script>