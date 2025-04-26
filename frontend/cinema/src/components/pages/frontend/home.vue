<template>
  <div>
    <Header />
    <main class="main-content">
      <!-- Phim Đang Chiếu -->
      <div class="container py-5">
        <h2 class="mb-4 text-center">🎥 Phim Đang Chiếu</h2>
        <div class="row">
          <div v-for="movie in nowShowing" :key="movie.id" class="col-md-4 mb-4">
            <div class="card h-100">
              <img :src="movie.posterUrl" class="card-img-top" alt="Poster" />
              <div class="card-body d-flex flex-column">
                <h5 class="card-title">{{ movie.title }}</h5>
                <p class="card-text">{{ movie.genre }}</p>
                <p class="card-text">Đánh giá: {{ movie.rating }}/10</p>

                <button class="btn btn-outline-primary mt-auto" @click="goToDetail(movie.id)">
                  Xem chi tiết
                </button>
              </div>
            </div>
          </div>
        </div>
      </div>

      <!-- Phim Sắp Chiếu -->
      <div class="container py-5">
        <h2 class="mb-4 text-center">🎬 Phim Sắp Chiếu</h2>
        <div class="row">
          <div v-for="movie in comingSoon" :key="movie.id" class="col-md-4 mb-4">
            <div class="card h-100">
              <img :src="movie.posterUrl" class="card-img-top" alt="Poster" />
              <div class="card-body d-flex flex-column">
                <h5 class="card-title">{{ movie.title }}</h5>
                <p class="card-text">{{ movie.genre }}</p>
                <p class="card-text">Đánh giá: {{ movie.rating }}/10</p>

                <button class="btn btn-outline-primary mt-auto" @click="goToDetail(movie.id)">
                  Xem chi tiết
                </button>
              </div>
            </div>
          </div>
        </div>
      </div>

      <!-- Phim Phổ Biến -->
      <div class="container py-5">
        <h2 class="mb-4 text-center">🔥 Phim Phổ Biến</h2>
        <div class="row">
          <div v-for="movie in popularMovies" :key="movie.id" class="col-md-4 mb-4">
            <div class="card h-100">
              <img :src="movie.posterUrl" class="card-img-top" alt="Poster" />
              <div class="card-body d-flex flex-column">
                <h5 class="card-title">{{ movie.title }}</h5>
                <p class="card-text">Đánh giá: {{ movie.rating }}/10</p>
                <button class="btn btn-outline-primary mt-auto" @click="goToDetail(movie.id)">
                  Xem chi tiết
                </button>
              </div>
            </div>
          </div>
        </div>
      </div>
    </main>
    <Footer />
  </div>
</template>

<script setup>
import Header from './Navbar.vue';
import Footer from './footer.vue';
import { ref, onMounted } from 'vue';
import { useRouter } from 'vue-router';
import ProductService from '../../../service/productService';

const router = useRouter();

const nowShowing = ref([]);
const comingSoon = ref([]);
const popularMovies = ref([]);

const goToDetail = (id) => {
  router.push(`/movies/${id}`);
};

onMounted(async () => {
  nowShowing.value = await ProductService.getNowShowingMovies();
  comingSoon.value = await ProductService.getComingSoonMovies();
  popularMovies.value = await ProductService.getPopularMovies();
});
</script>

<style scoped>
.card {
  transition: transform 0.3s ease;
}

.card:hover {
  transform: scale(1.05);
  box-shadow: 0 4px 12px rgba(0, 0, 0, 0.15);
}
</style>
