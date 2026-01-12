import { configureStore } from '@reduxjs/toolkit';
import referenceItemsReducer from './features/referenceItems/referenceItemsSlice';

const store = configureStore({
  reducer: {
    referenceItems: referenceItemsReducer,
  },
});

export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;

export default store;
